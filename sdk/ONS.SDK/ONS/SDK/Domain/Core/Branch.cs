using System;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Domain.Core {

    /// <summary>
    /// Representa a entidade de branch do core da plataforma.
    /// </summary>
    public class Branch : Model {
        public string SystemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartedAt { get; set; }
        public string Status { get; set; }

        public Branch() {
            this._Metadata = new Metadata() {
                Type="branch"
            };
        }
    }
}