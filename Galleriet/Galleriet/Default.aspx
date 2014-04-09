﻿<%@ Page Language="C#" ViewStateMode="Disabled" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Galleriet.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Galleriet - Eddy Proca</title>
</head>
<body>
    <form id="ImageUploaderForm" runat="server">
        <asp:PlaceHolder ID="MessageHolder" runat="server" Visible="false">
            <div ID="MessageBox">
                <asp:Label ID="SuccessMessage" runat="server"></asp:Label>
                <button ID="CloseMessage">✖</button>
            </div>
        </asp:PlaceHolder>
        <div>
            <asp:Image ID="Original" runat="server" Visible="false" />
        </div>
        <div>
            <asp:Repeater ID="ImageRepeater" runat="server" ItemType="System.String" SelectMethod="ImageRepeater_GetData">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%#: "Default.aspx?image=" + Item %>'>
                        <asp:Image runat="server" ImageUrl='<%#: "~/Images/Thumbs/" + Item %>' />
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div>
            <asp:ValidationSummary ID="ValidationSummary" runat="server" HeaderText="Fel inträffade! Korigera felet och försök igen." />
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Ett fel inträffade då bilden skulle överföras." Display="None"></asp:CustomValidator>
            <asp:FileUpload ID="ImageUploader" runat="server" Width="400" />
            <asp:RequiredFieldValidator ControlToValidate="ImageUploader" 
                Display="None" ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="En fil måste väljas."></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="ImageUploader" ValidationExpression=".*.(jp[e]?g|JP[E]?G|gif|GIF|png|PNG)$" 
                Display="None" ID="RegularExpressionValidator1" runat="server" 
                ErrorMessage="Endast bilder av typerna gif, jpeg eller png är tillåtna."></asp:RegularExpressionValidator>
        </div>
        <div>
            <asp:Button ID="UploadImage" runat="server" Text="Ladda upp" OnClick="UploadImage_Click" />
        </div>
    </form>
    <script src="Scripts/gallery.js"></script>
</body>
</html>
