using EnterpriseRAG.Application.Embeddings.Interfaces;
using EnterpriseRAG.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.Tokenizers;

namespace EnterpriseRAG.Infrastructure.Embedding
{
    public class EmbeddingService : IEmbeddingService
    {
        private readonly InferenceSession _Part;
        private readonly BertTokenizer _tokenizer;
        public EmbeddingService(
            IOptions<MiniLMOptions> options)
        {
            var config = options.Value;

            _Part =
                new InferenceSession(config.ModelPath);

            _tokenizer =
                BertTokenizer.Create(
                    config.VocabularyPath);
        }


        public async Task<float[]> GenerateEmbeddingAsync(string text)
        {
            // Tokenization

            var ids = _tokenizer.EncodeToIds(text);

            //if (ids.Count > 512)
            //{
            //    ids = ids.Take(512).ToList();
            //}


            // Add [CLS] and [SEP]

            var tokenIds =
                _tokenizer.BuildInputsWithSpecialTokens(
                    ids);

            //512
            //CLS 23,3,44,44 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0  SEP

            // attend only required embeding - paddings
            long[] attentionMask = Enumerable
                    .Repeat(
                        1L,
                        tokenIds.Count)
                    .ToArray();

            long[] tokenTypeIds = _tokenizer.CreateTokenTypeIdsFromSequences(
            ids).Select(x => (long)x).ToArray();

            var inputIdsTensor =
         new DenseTensor<long>(
             tokenIds
                 .Select(x => (long)x)
                 .ToArray(),
             new[] { 1, tokenIds.Count });

            var attentionTensor =
                new DenseTensor<long>(
                    attentionMask,
                    new[] { 1, attentionMask.Length });

            var tokenTypeTensor =
                new DenseTensor<long>(
                    tokenTypeIds,
                    new[] { 1, tokenTypeIds.Length });

            var inputs = new List<NamedOnnxValue>
                {
             NamedOnnxValue.CreateFromTensor(
                 "input_ids",
                 inputIdsTensor),

             NamedOnnxValue.CreateFromTensor(
                 "attention_mask",
                 attentionTensor),

             NamedOnnxValue.CreateFromTensor(
                 "token_type_ids",
                 tokenTypeTensor)
                };

            using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = _Part.Run(inputs);

            var tensor = results.First().AsTensor<float>();
            var embedding = MeanPooling(tensor);

            float magnitude =MathF.Sqrt(embedding.Sum(x => x * x));

            for (int i = 0; i < embedding.Length; i++)
            {
                embedding[i] /= magnitude;
            }

            return embedding;
        }

        private float[] MeanPooling(Tensor<float> tensor)
             {
                int tokens = tensor.Dimensions[1];

                int dimension = tensor.Dimensions[2];

            float[] embedding = new float[dimension];

            for (int d = 0; d < dimension; d++)
            {
                float sum = 0;

                for (int t = 0; t < tokens; t++)
                {
                    sum += tensor[0, t, d];
                }

                embedding[d] = sum / tokens;
            }

            return embedding;
        }

    }
}
 

