<%@ Page Language="C#" ViewStateMode="Disabled" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Galleriet.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Galleriet - Eddy Proca</title>
</head>
<body>
    <form id="ImageUploaderForm" runat="server">
        <div>
            <asp:Image ID="Original" runat="server" Visible="false" />
        </div>
        <div>
            <asp:Repeater ID="ImageRepeater" runat="server" ItemType="System.String" SelectMethod="ImageRepeater_GetData">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%#: "Default.aspx?image=" + Item.Substring(0) %>'>
                        <asp:Image runat="server" ImageUrl='<%#: "~/Images/Thumbs/" + Item %>' />
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div>
            <asp:FileUpload ID="ImageUploader" runat="server" />
            <asp:RequiredFieldValidator ControlToValidate="ImageUploader" 
                Display="Dynamic" ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="En fil måste väljas." Text="*"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="ImageUploader" ValidationExpression=".*.(jp[e]?g|JP[E]?G|gif|GIF|png|PNG)$" 
                Display="Dynamic" ID="RegularExpressionValidator1" runat="server" 
                ErrorMessage="Endast bilder av typerna gif, jpeg eller png är tillåtna." Text="*"></asp:RegularExpressionValidator>
        </div>
        <div>
            <asp:Button ID="UploadImage" runat="server" Text="Ladda upp" OnClick="UploadImage_Click" />
        </div>
    </form>
</body>
</html>
