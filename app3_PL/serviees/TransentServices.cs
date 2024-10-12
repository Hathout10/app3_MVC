
namespace app3.PL.serviees
{
    public class TransentServices : ItrenseintServes
    {
        public Guid Guid { get; set; }

        public TransentServices()
        {
            Guid = Guid.NewGuid();
        }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
