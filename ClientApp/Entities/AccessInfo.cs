using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Entities
{
    class AccessInfo
    {
        public string accessToken { get; set; }
        public string ownId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime deletedAt { get; set; }
        public DateTime expiredAt { get; set; }
        public int status { get; set; }

    }
}
