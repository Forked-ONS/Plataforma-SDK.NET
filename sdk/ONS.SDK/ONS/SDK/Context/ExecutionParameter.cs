using System;
using Microsoft.Extensions.Configuration;
using ONS.SDK.Configuration;
using ONS.SDK.Domain.ProcessMemmory;

namespace ONS.SDK.Context {

    /// <summary>
    /// Parâmetros de execução de uma serviço de negócio para atender um evento do sistema.
    /// </summary>
    public class ExecutionParameter 
    {
        /// <summary>
        /// Memória de processamento.
        /// </summary>
        public Memory Memory { get; internal set; }

        /// <summary>
        /// Evento da memória de processamento.
        /// </summary>
        public MemoryEvent MemoryEvent { 
            get {
                return Memory != null? Memory.Event : null;
            } 
        }

        /// <summary>
        /// Indica se a persistência dos dados deve ser executada de forma síncrona ou assíncrona, 
        /// após a execução do método de negócio.
        /// </summary>
        public bool SynchronousPersistence { get; internal set; }

        /// <summary>
        /// Nome do evento na memória de processamento, durante a execução do evento.
        /// </summary>
        public string EventName { 
            get {
                return MemoryEvent != null? MemoryEvent.Name : null;
            } 
        }

        /// <summary>
        /// Indica o branch de execução do evento.
        /// </summary>
        public string Branch { 
            get {
                return MemoryEvent != null? MemoryEvent.Branch : null;
            } 
        }

        /// <summary>
        /// Data de referência da execução do evento.
        /// </summary>
        public DateTime? ReferenceDate { 
            get {
                return MemoryEvent != null && MemoryEvent.ReferenceDate.HasValue ? 
                    MemoryEvent.ReferenceDate.Value : default(DateTime?);
            } 
        }

        /// <summary>
        /// Informações de reprocessamento do evento, caso esse seja de reprocessamento.
        /// </summary>
        public Reprocess Reprocess {
            get {
                return MemoryEvent != null? MemoryEvent.Reprocess : null;
            }
        }

        /// <summary>
        /// Identificador da instância de execução do evento.
        /// </summary>
        public string InstanceId { get; internal set; }
        
        public ExecutionParameter()
        {        
        }

        public override string ToString() {
            return $"{this.GetType().Name}[EventName={EventName}, Branch={Branch}, SynchronousPersistence={SynchronousPersistence}, " + 
                $"ReferenceDate={ReferenceDate}, InstanceId={InstanceId}, Reprocess={Reprocess}]";
        }
    }
}