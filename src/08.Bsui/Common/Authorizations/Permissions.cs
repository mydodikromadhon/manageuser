namespace CRUD.ManagementUser.Bsui.Common.Authorizations;

public static class Permissions
{
    #region Essential Permissions
    public const string CRUD_ManagementUser_Audit_Index = "CRUD.ManagementUser.audit.index";
    public const string CRUD_ManagementUser_Audit_View = "CRUD.ManagementUser.audit.view";
    public const string CRUD_ManagementUser_HealthCheck_View = "CRUD.ManagementUser.healthcheck.view";
    #endregion Essential Permissions

    #region Business Permissions
    public const string CRUD_ManagementUser_App_Create = "CRUD.ManagementUser.app.create";
    public const string CRUD_ManagementUser_App_Update = "CRUD.ManagementUser.app.update";
    public const string CRUD_ManagementUser_App_Delete = "CRUD.ManagementUser.app.delete";
    public const string CRUD_ManagementUser_App_Index = "CRUD.ManagementUser.app.index";
    public const string CRUD_ManagementUser_App_View = "CRUD.ManagementUser.app.view";

    public const string CRUD_ManagementUser_Ticket_Handle = "CRUD.ManagementUser.ticket.handle";
    public const string CRUD_ManagementUser_Ticket_Remove = "CRUD.ManagementUser.ticket.remove";

    public const string CRUD_ManagementUser_SourceCode_View = "CRUD.ManagementUser.sourcecode.view";

    public const string CRUD_ManagementUser_Document_Create = "CRUD.ManagementUser.document.create";
    public const string CRUD_ManagementUser_Document_Delete = "CRUD.ManagementUser.document.delete";
    public const string CRUD_ManagementUser_Document_Index = "CRUD.ManagementUser.document.index";
    public const string CRUD_ManagementUser_Document_Download = "CRUD.ManagementUser.document.download";
    #endregion Business Permissions

    public static readonly string[] All = new string[]
    {
        #region Essential Permissions
        CRUD_ManagementUser_Audit_Index,
        CRUD_ManagementUser_Audit_View,
        CRUD_ManagementUser_HealthCheck_View,
        #endregion Essential Permissions

        #region Business Permissions
        CRUD_ManagementUser_App_Create,
        CRUD_ManagementUser_App_Update,
        CRUD_ManagementUser_App_Delete,
        CRUD_ManagementUser_App_Index,
        CRUD_ManagementUser_App_View,

        CRUD_ManagementUser_Ticket_Handle,
        CRUD_ManagementUser_Ticket_Remove,

        CRUD_ManagementUser_SourceCode_View,

        CRUD_ManagementUser_Document_Create,
        CRUD_ManagementUser_Document_Delete,
        CRUD_ManagementUser_Document_Index,
        CRUD_ManagementUser_Document_Download
        #endregion Business Permissions
    };
}
