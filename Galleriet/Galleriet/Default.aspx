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
        <asp:RequiredFieldValidator ControlToValidate="ImageUploader" Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ErrorMessage="En fil måste väljas." Text="*"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ControlToValidate="ImageUploader" ValidationExpression="\.(jp[e]?g|JP[E]?G|gif|GIF|png|PNG)$" Display="Dynamic" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Endast bilder av typerna gif, jpeg eller png är tillåtna." Text="*"></asp:RegularExpressionValidator>
        <asp:Button ID="UploadImage" runat="server" Text="Ladda upp" />
    </div>
    </form>
</body>
</html>
