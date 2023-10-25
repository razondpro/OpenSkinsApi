namespace OpenSkinsApi.Infrastructure.Persistence.Seed
{
    using Newtonsoft.Json;
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
                        null,
                        Name.Create(skin.Name),
                        Money.Create(skin.Price),
                        (Type)skin.Type,
                        (Color)skin.Color
                    );

                    if (skin.IsAvailable)
                        s.MakeItAvailable();
                    else
                        s.MakeItUnavailable();

                    s.CreatedOn = DateTime.UtcNow;

                    skins.Add(s);

                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return skins;
        }

        public static List<Owner> LoadOwners()
        {
            var owner = Owner.Create(
                null,
                Email.Create("  ")
            );

            owner.CreatedOn = DateTime.UtcNow;

            return new List<Owner> { owner };
        }
    }

    internal record SkinData(
        string Name,
        int Type,
        int Color,
        decimal Price,
        bool IsAvailable
    );
}