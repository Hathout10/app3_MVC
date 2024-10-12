
namespace app3.PL.serviees
{
    public class singeltonServies : IsingeltonServies
    {
        public Guid Guid { get; set; }

        public singeltonServies()
        {
            Guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
           return Guid.ToString();
        }
    }
}
