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
                          <asp:TextBox ID="txt00" class="cTxt" runat="server"  onkeypress="javascript:return KeyPress(event,'txt00')"  onkeyup="javascript:return KeyUp(event,'txt00')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt01" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt01')"  onkeyup="javascript:return KeyUp(event,'txt01')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt02" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt02')"  onkeyup="javascript:return KeyUp(event,'txt02')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt10" class="cTxt" runat="server"  onkeypress="javascript:return KeyPress(event,'txt10')"  onkeyup="javascript:return KeyUp(event,'txt10')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt11" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt11')"  onkeyup="javascript:return KeyUp(event,'txt11')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt12" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt12')"  onkeyup="javascript:return KeyUp(event,'txt12')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt20" class="cTxt" runat="server"  onkeypress="javascript:return KeyPress(event,'txt20')"  onkeyup="javascript:return KeyUp(event,'txt20')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt21" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt21')"  onkeyup="javascript:return KeyUp(event,'txt21')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt22" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt22')"  onkeyup="javascript:return KeyUp(event,'txt22')"  MaxLength="1"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro1" class="box">
                          <asp:TextBox ID="txt03" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt03')"  onkeyup="javascript:return KeyUp(event,'txt03')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt04" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt04')"  onkeyup="javascript:return KeyUp(event,'txt04')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt05" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt05')"  onkeyup="javascript:return KeyUp(event,'txt05')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt13" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt13')"  onkeyup="javascript:return KeyUp(event,'txt13')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt14" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt14')"  onkeyup="javascript:return KeyUp(event,'txt14')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt15" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt15')"  onkeyup="javascript:return KeyUp(event,'txt15')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt23" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt23')"  onkeyup="javascript:return KeyUp(event,'txt23')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt24" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt24')"  onkeyup="javascript:return KeyUp(event,'txt24')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt25" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt25')"  onkeyup="javascript:return KeyUp(event,'txt25')"  MaxLength="1"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro2" class="box">
                          <asp:TextBox ID="txt06" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt06')"  onkeyup="javascript:return KeyUp(event,'txt06')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt07" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt07')"  onkeyup="javascript:return KeyUp(event,'txt07')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt08" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt08')"  onkeyup="javascript:return KeyUp(event,'txt08')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt16" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt16')"  onkeyup="javascript:return KeyUp(event,'txt16')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt17" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt17')"  onkeyup="javascript:return KeyUp(event,'txt17')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt18" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt18')"  onkeyup="javascript:return KeyUp(event,'txt18')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt26" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt26')"  onkeyup="javascript:return KeyUp(event,'txt26')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt27" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt27')"  onkeyup="javascript:return KeyUp(event,'txt27')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt28" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt28')"  onkeyup="javascript:return KeyUp(event,'txt28')"  MaxLength="1"></asp:TextBox>
                      </div>

                      <!-- FILA 2 -->
                      <div id="recuadro3" class="box">
                          <asp:TextBox ID="txt30" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt30')"  onkeyup="javascript:return KeyUp(event,'txt30')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt31" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt31')"  onkeyup="javascript:return KeyUp(event,'txt31')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt32" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt32')"  onkeyup="javascript:return KeyUp(event,'txt32')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt40" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt40')"  onkeyup="javascript:return KeyUp(event,'txt40')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt41" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt41')"  onkeyup="javascript:return KeyUp(event,'txt41')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt42" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt42')"  onkeyup="javascript:return KeyUp(event,'txt42')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt50" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt50')"  onkeyup="javascript:return KeyUp(event,'txt50')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt51" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt51')"  onkeyup="javascript:return KeyUp(event,'txt51')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt52" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt52')"  onkeyup="javascript:return KeyUp(event,'txt52')"  MaxLength="1"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro4" class="box">
                          <asp:TextBox ID="txt33" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt33')"  onkeyup="javascript:return KeyUp(event,'txt33')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt34" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt34')"  onkeyup="javascript:return KeyUp(event,'txt34')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt35" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt35')"  onkeyup="javascript:return KeyUp(event,'txt35')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt43" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt43')"  onkeyup="javascript:return KeyUp(event,'txt43')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt44" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt44')"  onkeyup="javascript:return KeyUp(event,'txt44')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt45" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt45')"  onkeyup="javascript:return KeyUp(event,'txt45')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt53" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt53')"  onkeyup="javascript:return KeyUp(event,'txt53')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt54" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt54')"  onkeyup="javascript:return KeyUp(event,'txt54')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt55" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt55')"  onkeyup="javascript:return KeyUp(event,'txt55')"  MaxLength="1"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro5" class="box">
                          <asp:TextBox ID="txt36" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt36')"  onkeyup="javascript:return KeyUp(event,'txt36')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt37" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt37')"  onkeyup="javascript:return KeyUp(event,'txt37')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt38" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt38')"  onkeyup="javascript:return KeyUp(event,'txt38')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt46" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt46')"  onkeyup="javascript:return KeyUp(event,'txt46')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt47" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt47')"  onkeyup="javascript:return KeyUp(event,'txt47')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt48" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt48')"  onkeyup="javascript:return KeyUp(event,'txt48')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt56" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt56')"  onkeyup="javascript:return KeyUp(event,'txt56')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt57" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt57')"  onkeyup="javascript:return KeyUp(event,'txt57')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt58" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt58')"  onkeyup="javascript:return KeyUp(event,'txt58')"  MaxLength="1"></asp:TextBox>
                      </div>

                      <!--  FILA 3-->
                      <div class="box">
                          <asp:TextBox ID="txt60" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt60')"  onkeyup="javascript:return KeyUp(event,'txt60')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt61" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt61')"  onkeyup="javascript:return KeyUp(event,'txt61')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt62" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt62')"  onkeyup="javascript:return KeyUp(event,'txt62')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt70" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt70')"  onkeyup="javascript:return KeyUp(event,'txt70')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt71" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt71')"  onkeyup="javascript:return KeyUp(event,'txt71')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt72" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt72')"  onkeyup="javascript:return KeyUp(event,'txt72')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt80" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt80')"  onkeyup="javascript:return KeyUp(event,'txt80')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt81" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt81')"  onkeyup="javascript:return KeyUp(event,'txt81')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt82" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt82')"  onkeyup="javascript:return KeyUp(event,'txt82')"  MaxLength="1"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div class="box">
                          <asp:TextBox ID="txt63" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt63')"  onkeyup="javascript:return KeyUp(event,'txt63')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt64" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt64')"  onkeyup="javascript:return KeyUp(event,'txt64')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt65" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt65')"  onkeyup="javascript:return KeyUp(event,'txt65')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt73" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt73')"  onkeyup="javascript:return KeyUp(event,'txt73')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt74" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt74')"  onkeyup="javascript:return KeyUp(event,'txt74')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt75" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt75')"  onkeyup="javascript:return KeyUp(event,'txt75')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt83" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt83')"  onkeyup="javascript:return KeyUp(event,'txt83')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt84" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt84')"  onkeyup="javascript:return KeyUp(event,'txt84')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt85" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt85')"  onkeyup="javascript:return KeyUp(event,'txt85')"  MaxLength="1"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div class="box">
                          <asp:TextBox ID="txt66" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt66')"  onkeyup="javascript:return KeyUp(event,'txt66')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt67" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt67')"  onkeyup="javascript:return KeyUp(event,'txt67')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt68" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt68')"  onkeyup="javascript:return KeyUp(event,'txt68')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt76" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt76')"  onkeyup="javascript:return KeyUp(event,'txt76')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt77" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt77')"  onkeyup="javascript:return KeyUp(event,'txt77')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt78" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt78')"  onkeyup="javascript:return KeyUp(event,'txt78')"  MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt86" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt86')"  onkeyup="javascript:return KeyUp(event,'txt86')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt87" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt87')"  onkeyup="javascript:return KeyUp(event,'txt87')"  MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt88" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt88')"  onkeyup="javascript:return KeyUp(event,'txt88')"  MaxLength="1"></asp:TextBox>
                      </div>

                  </div>
              </div>
          </div>
        </div>
     </div>
    </form>

    <script>
        function KeyPress(e, id) {
            console.log(id);
              var key;
            if (window.event) 
            {
                key = e.keyCode;
            }
            else if (e.which) 
            {
                key = e.which;
            }
            if (key > 48 && key < 58)  {
                return true;
            }
            return false; 
        }

        function KeyUp(e, id) {
            console.log(id);
            var numero = document.getElementById(id).value;
                if (/^([0-9])*$/.test(numero))
                    alert("El valor " + numero );
            var key;
            if (window.event) 
            {
                key = e.keyCode;
            }
            else if (e.which) 
            {
                key = e.which;
            }
            if (key > 48 && key < 58)   {
                return true;
            }
            return false; 
         
        }
    </script>
</body>

 
</html>
