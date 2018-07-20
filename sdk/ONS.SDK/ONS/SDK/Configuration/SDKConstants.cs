namespace ONS.SDK.Configuration
{
    /// <summary>
    /// Indica as constantes do SDK.
    /// </summary>
    public class SDKConstants
    {
        /// <summary>
        /// Parâmetro de paginação do serviço de consulta no domínio, indica o tamanho da página.
        /// </summary>
        public static string ParamNamePageSize = "page_size";

        /// <summary>
        /// Parâmetro que indica de paginação do serviço de consulta no domínio, indica a página corrente.
        /// </summary>
        public static string ParamNamePage = "page";

        /// <summary>
        /// Indica o nome do filtro padrão do domínio, para retornar todos os dados de uma entidade.
        /// </summary>
        public static string FilterNameAll = "all";

        /// <summary>
        /// Nome do Branch principal do sistema.
        /// </summary>
        public static string BranchMaster = "master";

        /// <summary>
        /// Indica o nome do escope de reprodução.
        /// </summary>
        public static string Reproduction = "reproduction";
    }
}