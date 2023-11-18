using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.Models.Dto
{
    public class StorageAreaDto : BaseDto
    {

        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private int? slave_addr;

        public int? SLAVE_ADDR
        {
            get { return slave_addr; }
            set { slave_addr = value; OnPropertyChanged(); }
        }

        private string func_code;

        public string FUNC_CODE
        {
            get { return func_code; }
            set { func_code = value; OnPropertyChanged(); }
        }

        private int? start_req;

        public int? START_REQ
        {
            get { return start_req; }
            set { start_req = value; OnPropertyChanged(); }
        }

        private int? length;

        public int? LENGTH
        {
            get { return length; }
            set { length = value; OnPropertyChanged(); }
        }



    }
}
