using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traductor
{
    class Palabra
    {
        private string sigEspanol;
        private string sigIngles;

        public string SigEspanol
        {
            get => sigEspanol;
            set => sigEspanol = value;
        }
        
        public string SigIngles
        {
            get => sigIngles;
            set => sigIngles = value;
        }

    }
}
