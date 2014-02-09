<%@ Page Language="C#" ViewStateMode="Disabled" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Galleriet.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Galleriet - Eddy Proca</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload ID="ImageUploader" runat="server" />
        <asp:Button ID="UploadImage" runat="server" Text="Ladda upp" />
    </div>
    </form>
</body>
</html>
