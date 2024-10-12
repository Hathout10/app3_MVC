
namespace app3.PL.serviees
{
    public class ScopedServie : IscopedServices
    {
        public Guid Guid { get; set ; }

        public ScopedServie()
        {
            Guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Guid.ToString(); 
        }
    }
}
