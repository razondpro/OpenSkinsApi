namespace OpenSkinsApi.Infrastructure.Persistence.Seed
{
    using Newtonsoft.Json;
    using OpenSkinsApi.Domain;
    using OpenSkinsApi.Modules.Skins.Domain.Entities;
    using OpenSkinsApi.Modules.Skins.Domain.Enums;
    using OpenSkinsApi.Modules.Skins.Domain.ValueObjects;

    public static class Seeder
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
                    skins.Add(
                        Skin.Create(
                            new UniqueIdentity(skin.Id),
                            Name.Create(skin.Name),
                            Money.Create(skin.Price.Amount),
                            (Type)skin.Type,
                            (Color)skin.Color
                        )
                    );
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
        Price Price,
        bool IsAvailable,
        DateTime CreatedOn,
        DateTime? LastModifiedOn
    );

    internal record Price(
        decimal Amount,
        int Currency
    );
}