<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayGame.aspx.cs" Inherits="SudokuForAll.View.PlayGame" %>

<!DOCTYPE html>




<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
   <link href="~/Content/bootstrap.css" rel="stylesheet" />
   <link href="~/Content/bootstrap-theme.css" rel="stylesheet" />
   <link href="~/Content/css/Global.css" rel="stylesheet" />
    <link href="~/Content/css/style.css" rel="stylesheet" />
   <script src="~/Scripts/jquery-3.3.1.min.js" type="text/javascript"></script>
   <script src="~/Content/js/Global.js" type="text/javascript"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">


          <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>

            </div>
            <div class="navbar-collapse collapse">
                <img id="logSudoku" class="logoInitial" src="../Content/imagen/logo.png"/>
                <label class="navbar-brand"> Sudoku de Todos </label>
                <ul class="nav navbar-nav">
                    <li><a href="/Home/Index" id="inicio"></a></li>
                    <li><a href="/Home/Contact" id="entrar"></a></li>
                </ul>
            </div>
        </div>
    </div>


    <div>
         <div class="container-fluid menuVertical" >
    <div class="dropdown">
        <button class="dropbtn btn btn-primary">Menu</button>
        <div class="dropdown-content">
            <a href="#">AbrirJuego</a>
            <a href="#">Crear Juego</a>
            <a href="#"></a>
        </div>
    </div>
</div>



<br /><br />
      <div class="container-fluid">
          <div class="container">
              <div class="grid">
                  <div class="innerGrid">
                      <!--FILA 1  -->
                      <div id="recuadro0" class="box">
                          <asp:TextBox ID="txt00" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt01" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt02" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt10" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt11" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt12" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt20" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt21" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt22" class="cTxt" runat="server"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro1" class="box">
                          <asp:TextBox ID="txt03" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt04" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt05" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt13" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt14" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt15" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt23" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt24" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt25" class="cTxt" runat="server"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro2" class="box">
                          <asp:TextBox ID="txt06" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt07" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt08" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt16" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt17" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt18" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt26" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt27" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt28" class="cTxt" runat="server"></asp:TextBox>
                      </div>

                      <!-- FILA 2 -->
                      <div id="recuadro3" class="box">
                          <asp:TextBox ID="txt30" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt31" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt32" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt40" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt41" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt42" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt50" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt51" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt52" class="cTxt" runat="server"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro4" class="box">
                          <asp:TextBox ID="txt33" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt34" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt35" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt43" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt44" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt45" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt53" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt54" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt55" class="cTxt" runat="server"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro5" class="box">
                          <asp:TextBox ID="txt36" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt37" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt38" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt46" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt47" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt48" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt56" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt57" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt58" class="cTxt" runat="server"></asp:TextBox>
                      </div>

                      <!--  FILA 3-->
                      <div class="box">
                          <asp:TextBox ID="txt60" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt61" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt62" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt70" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt71" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt72" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt80" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt81" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt82" class="cTxt" runat="server"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div class="box">
                          <asp:TextBox ID="txt63" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt64" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt65" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt73" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt74" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt75" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt83" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt84" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt85" class="cTxt" runat="server"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div class="box">
                          <asp:TextBox ID="txt66" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt67" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt68" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt76" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt77" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt78" class="cTxt" runat="server"></asp:TextBox>
                          <asp:TextBox ID="txt86" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt87" class="cTxt" runat="server"></asp:TextBox> <asp:TextBox ID="txt88" class="cTxt" runat="server"></asp:TextBox>
                      </div>

                  </div>
              </div>
          </div>
        </div>
     </div>
    </form>
</body>

 
</html>
