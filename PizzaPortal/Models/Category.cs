namespace PizzaPortal.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CategoryAddedDate { get; set; }

        public ICollection<Pizza> Pizzas { get; set; }
    }
}
