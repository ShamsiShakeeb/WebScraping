using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScrapingSite.Models
{
    public class ScrapingInformationModel
    {
        public class Zenith
        {
           public Zenith()
           {
                LoginForm = new Dictionary<string, string>();
                ScarpingInformation = new Dictionary<string, string>();

                LoginForm.Add("LoginLog", "acon");
                LoginForm.Add("LoginPwd", "BSAcon22");
                ScarpingInformation.Add("LoginLink", "https://fo-asia.ttinteractive.com/Zenith/FrontOffice/(S(ce8ab7e8c07d4ad181385024b66d5c0d))/USBangla/en-GB/travelAgency/Login");
                ScarpingInformation.Add("FromName", "form1");
                ScarpingInformation.Add("htmlTag", "tr");
                ScarpingInformation.Add("htmlClass", "line-body");

                NavigationLink = "https://fo-asia.ttinteractive.com/newui/aerien/recettesco/Facturation_ClientCpt.asp?BoolAffCriteres=&hAction=seek&htypeclient=&NAV=1&Flag=&ID_CaisseVente=&codeagence=10411943&nom_controle=&CdVendeur=&DateEcritureMin={0}&DateEcritureMax={1}&TypeFiltre=0&Id_modePaiement=1&ListID_TypeOrigineDossier=&ListID_TypeEnCaisse=&LstID_Vendeur=&LstID_PointVente=&FiltrageEcritureCompagnie=&BoolAffDetail=True&NbReponseParPage=2000&DisplayTaxes=False&cbDevise=BDT";
           }
           public Dictionary<string, string> LoginForm { set; get; }
           public Dictionary<string, string> ScarpingInformation { set; get; }
           public string NavigationLink { set; get; }

        }
    }
}
