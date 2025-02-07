namespace BeanOfTheDay.Library.Models;

public class Order
{
    public int OrderId { get; set; }
    public string? Name { get; set; }
    public string? DeliveryAddress { get; set; }
    public string? DeliveryInstructions { get; set; }
    public List<Bean>? OrderedBeans { get; set; }
}