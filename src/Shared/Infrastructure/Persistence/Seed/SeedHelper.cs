namespace OpenSkinsApi.Infrastructure.Persistence.Seed
{
    using Newtonsoft.Json;
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Skins.Domain.Entities;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

    public static class SeedHelper
    {
        public static List<Skin> LoadSkinsFromJsonFile()
        {
            var skins = new List<Skin>();

            try
            {
                using StreamReader reader = new("skins.json");
                string json = reader.ReadToEnd();

                var data = JsonConvert.DeserializeObject<List<SkinData>>(json);
                //JsonConvert supports only primitive types, so we need to convert them manually because of ValueObjectss
                data?.ForEach(skin =>
                {
                    var s = Skin.Create(
                        new UniqueIdentity(skin.Id),
                        Name.Create(skin.Name),
                        Money.Create(skin.Price),
                        (Type)skin.Type,
                        (Color)skin.Color
                    );

                    if (skin.IsAvailable)
                        s.MakeItAvailable();
                    else
                        s.MakeItUnavailable();

                    skins.Add(s);

                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return skins;
        }
    }

    internal record SkinData(
        Guid Id,
        string Name,
        int Type,
        int Color,
        decimal Price,
        bool IsAvailable,
        DateTime CreatedOn,
        DateTime? LastModifiedOn
    );
}