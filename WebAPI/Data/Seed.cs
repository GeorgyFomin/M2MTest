using Entities.Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.MsSql;

namespace WebAPI.Data
{
    public class Seed
    {
        /// <summary>
        /// Хранит ссылку на генератор случайных чисел.
        /// </summary>
        static public readonly Random random = new();
        private static readonly List<Ingredient> ingredients = GetRandomIngredients(random.Next(5, 20), random);
        static List<Ingredient> GetIngredients()
        {
            List<Ingredient> list = new();
            // Число ингредиентов в списке.
            int nIngr = random.Next(2, ingredients.Count);
            Ingredient ingr;
            // Помещаем в список list разные случайно выбранные ингредиенты из полного списка ingredients в количестве nIngr.
            for (int i = 0; i < nIngr; i++)
            {
                do
                {
                    ingr = ingredients[random.Next(ingredients.Count)];
                } while (list.Contains(ingr));
                list.Add(ingr);
            }
            return list;
        }
        /// <summary>
        /// Возвращает список случайных продуктов.
        /// </summary>
        /// <param name="v">Число продуктов.</param>
        /// <param name="random">Генератор случайных чисел.</param>
        /// <returns></returns>
        private static List<Product> GetRandomProducts(int v, Random random)
        {
            return new(Enumerable.Range(0, v).Select(index => new Product()
            {
                Price = random.Next(1, 100),
                Weight = random.Next(1, 100),
                Name = GetRandomString(random.Next(1, 6), random)
            }).ToList());
        }
        static List<Product> GetProducts()
        {
            List<Product> products = GetRandomProducts(random.Next(3, 5), random);
            foreach (Product product in products)
            {
                List<Ingredient> ingredients = GetIngredients();
                List<ProductIngredient> productIngredients = new();
                foreach (Ingredient ingredient in ingredients)
                {
                    productIngredients.Add(new ProductIngredient() { Product = product, Ingredient = ingredient });
                }
                product.ProductsIngredients = productIngredients;
            }
            return products;
        }
        /// <summary>
        /// Возвращает список случайных ингредиентов.
        /// </summary>
        /// <param name="v">Число ингредиентов.</param>
        /// <param name="random">Генератор случайных чисел.</param>
        /// <returns></returns>
        private static List<Ingredient> GetRandomIngredients(int v, Random random) =>
                new(Enumerable.Range(0, v).Select(index => new Ingredient()
                {
                    Name = GetRandomString(random.Next(3, 6), random)
                }
            ).ToList());
        /// <summary>
        /// Генерирует случайную строку из латинских букв нижнего регистра.
        /// </summary>
        /// <param name="length">Длина строки.</param>
        /// <param name="random">Генератор случайных чисел.</param>
        /// <returns></returns>
        public static string GetRandomString(int length, Random random) => new(Enumerable.Range(0, length).Select(x => (char)random.Next('a', 'z' + 1)).ToArray());
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>());
            if (context == null || context.Products == null)
            {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                throw new ArgumentNullException("Null RazorDataContext");
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
            }

            if (context.Ingredients != null)
                foreach (Ingredient ingredient in context.Ingredients)
                    context.Ingredients.Remove(ingredient);
            foreach (Product product in context.Products)
            {
                context.Products.Remove(product);
            }
            context.SaveChanges();
            if (context.Ingredients!=null)
            {
                context.Ingredients.AddRange(ingredients);
                context.SaveChanges();
            }
            context.Products.AddRange(GetProducts());
            context.SaveChanges();
        }
    }
}
