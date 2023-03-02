namespace MagicTord_N_SondreTheWebAPI.Services
{
    using System.Linq;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// SchemaFilter to filter out classes in swaggers schemas
    /// </summary>
    public class SchemaFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var dtoNames = new[] { "CharacterDto", "CharacterPutDto", "CharacterPostDto", "MovieDto", "MoviePutDto", "MoviePostDto", "FranchiseDto", "FranchisePutDto", "FranchisePostDto" };
            foreach (var schema in swaggerDoc.Components.Schemas.ToList())
            {
                if (!dtoNames.Contains(schema.Key))
                {
                    swaggerDoc.Components.Schemas.Remove(schema.Key);
                }
            }
        }
    }
}
