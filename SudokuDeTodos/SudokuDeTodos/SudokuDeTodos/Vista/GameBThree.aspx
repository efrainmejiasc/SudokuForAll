<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameBThree.aspx.cs" Inherits="SudokuDeTodos.Vista.GameBThree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/css/global.css" rel="stylesheet" />
    <link href="~/Content/css/style.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Content/js/global.js" type="text/javascript"></script>
    <script src="../Content/js/game.js" type="text/javascript"></script>
    <title>Candidatos Individuales</title>
</head>
   <body>

       <form id="form1" runat="server">

             <asp:HiddenField ID="idTxt" runat="server" />
             <asp:HiddenField ID="number" runat="server" />
            <asp:HiddenField ID="number2" runat="server" />

            <div id="banner" class="divBanner container-fluid">
                        <img id="logSudoku"  class="logoInitial"/>
                          <ul class="row" style="margin-left:16%">   
                           <li style="list-style:none;"><a href="javascript:void(0)" id="sudoku"  class="listBanner">Sudoku de Todos</a></li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           <li style="list-style:none;"><a href="/Home/Index" id="inicio" class="listBanner">Inicio</a></li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           <li style="list-style:none;"><a href="/Home/Contact" id="entrar"  class="listBanner">Entrar</a></li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                         </ul>
             </div> 
           
          <div class="container-fluid">

               <div class="btn-group btnGroupA1" role="group" aria-label="Basic example">
                     <asp:Button ID="btnAA" runat="server" Text="A" cssClass="btn btn-success" OnClick="btnAA_Click"/>
                     <asp:Button ID="btnEE" runat="server" Text="E" cssClass="btn btn-danger" OnClick="btnEE_Click" />
                     <asp:Button ID="btnRR" runat="server" Text="R" cssClass="btn btn-warning" OnClick="btnEE_Click" />
                     <input id="image" type="button" class="btn btn-success arrowLeft" onClick="Navegacion('GameBTwo.aspx');"/>
                </div>
                <div id="area" align="center" class="divArea"> 
                   <div style="display:inline;" ><asp:TextBox ID="btnA" class="txtArea"  runat="server"  readonly ="true" style="text-align: center"></asp:TextBox>  <asp:ImageButton ID="btnC" runat="server"   class="txtArea"/> <asp:TextBox ID="btnB" class="txtArea" runat="server"  readonly="true" style="text-align: center"></asp:TextBox> </div><br />
                   <div style="display:inline;"> <asp:TextBox  ID="btnF" class="txtArea"  runat ="server" readonly="true" style="text-align: center"></asp:TextBox>  <asp:TextBox ID="btnE" class="txtArea" runat="server" readonly="true" style="text-align: center"></asp:TextBox>  <asp:TextBox ID="btnG" class="txtArea" runat="server"  readonly="true" style="text-align: center"></asp:TextBox> </div>
             </div>
                  <input type="text" id="txtNota" class="txtNota"/>
          

              
               <div style="float:right;margin-right:19%" class="btn bg-light">
                <div class="btn-group btn" role="group" aria-label="Basic example">
                <input id="" type="button" value="" class="btn bg-light white" onclick="Marcador('btn bg-light whiteLar', 'white');"/>
                     <input id="" type="button" value="" class="btn azulOscuro" onclick="Marcador('btn azulOscuroX whiteLar', 'dodgerblue');"/>
                     <input id="" type="button" value="" class="btn azulClaro" onclick="Marcador('btn azulClaroX whiteLar', 'cornflowerblue');"/>
                     <input id="" type="button" value="" class="btn rojoClaro" onclick="Marcador('btn rojoClaroX whiteLar', 'hotpink');"/>
                     <input id="" type="button" value="" class="btn rojoOscuro" onclick="Marcador('btn rojoOscuroX whiteLar', 'crimson');"/>
                     <input id="" type="button" value="" class="btn verdeClaro" onclick="Marcador('btn verdeClaroX whiteLar', 'palegreen');"/>
                     <input id="" type="button" value="" class="btn verdeOscuro" onclick="Marcador('btn verdeOscuroX whiteLar', 'darkolivegreen');"/>
                     <input id="" type="button" value="" class="btn naranjaClaro" onclick="Marcador('btn naranjaClaroX whiteLar', 'bisque');"/>
                     <input id="" type="button" value="" class="btn orange" onclick="Marcador('btn orangeX whiteLar', 'orange');"/>
                     <input id="marcador" type="button" value="" class="btn bg-light whiteLar"/></div>
                  </div>
               </div>
                <br /><br /> <br />
              <div class="container" style="margin-left:20%;">

               <div class="btn-group btnGroupMarket" role="group" aria-label="Basic example">
                     <asp:Button ID="lbl1" runat="server" Text="0" cssClass="btn btn lblCount" />
                     <asp:Button ID="lbl2" runat="server" Text="0" cssClass="btn btn lblCount" />
                     <asp:Button ID="lbl3" runat="server" Text="0" cssClass="btn btn lblCount"/>
                     <asp:Button ID="lbl4" runat="server" Text="0" cssClass="btn btn lblCount"/>
                     <asp:Button ID="lbl5" runat="server" Text="0" cssClass="btn btn lblCount" />
                     <asp:Button ID="lbl6" runat="server" Text="0" cssClass="btn btn lblCount"/>
                     <asp:Button ID="lbl7" runat="server" Text="0" cssClass="btn btn lblCount"/>
                     <asp:Button ID="lbl8" runat="server" Text="0" cssClass="btn btn lblCount"/>
                     <asp:Button ID="lbl9" runat="server" Text="0" cssClass="btn btn lblCount" />
               </div>

                    <div class="btn-group btnGroupMarket" role="group" aria-label="Basic example">
                     <asp:Button ID="btn1" runat="server" Text="1" cssClass="btn btn" OnClick="btn1_Click"/>
                     <asp:Button ID="btn2" runat="server" Text="2" cssClass="btn btn" OnClick="btn1_Click"/>
                     <asp:Button ID="btn3" runat="server" Text="3" cssClass="btn btn" OnClick="btn1_Click"/>
                     <asp:Button ID="btn4" runat="server" Text="4" cssClass="btn btn" OnClick="btn1_Click"/>
                     <asp:Button ID="btn5" runat="server" Text="5" cssClass="btn btn" OnClick="btn1_Click"/>
                     <asp:Button ID="btn6" runat="server" Text="6" cssClass="btn btn" OnClick="btn1_Click"/>
                     <asp:Button ID="btn7" runat="server" Text="7" cssClass="btn btn" OnClick="btn1_Click"/>
                     <asp:Button ID="btn8" runat="server" Text="8" cssClass="btn btn" OnClick="btn1_Click"/>
                     <asp:Button ID="btn9" runat="server" Text="9" cssClass="btn btn" OnClick="btn1_Click" />
                   
                 </div>

              </div>


       <div class="container-fluid" style=" margin-top:5%;" >
     
             <div class="container" align="center" style="margin-left:16%;float:left;width:500px;">
                  <div style="align-content:center">           
                     <label id="numero" class="nameGrid2">Candidatos</label>
                </div>
              <div class="grid">
                  <div class="innerGrid">
       
                      <!--FILA 1  -->
                      <div id="recuadro0" class="box">
                          <asp:TextBox ID="txt00" class="cTxt" runat="server"  onkeypress="javascript:return KeyPress(event,'txt00')"  onkeyup="javascript:return KeyUp(event,'txt00')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt01" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt01')"  onkeyup="javascript:return KeyUp(event,'txt01')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt02" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt02')"  onkeyup="javascript:return KeyUp(event,'txt02')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt10" class="cTxt" runat="server"  onkeypress="javascript:return KeyPress(event,'txt10')"  onkeyup="javascript:return KeyUp(event,'txt10')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt11" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt11')"  onkeyup="javascript:return KeyUp(event,'txt11')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt12" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt12')"  onkeyup="javascript:return KeyUp(event,'txt12')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt20" class="cTxt" runat="server"  onkeypress="javascript:return KeyPress(event,'txt20')"  onkeyup="javascript:return KeyUp(event,'txt20')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt21" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt21')"  onkeyup="javascript:return KeyUp(event,'txt21')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt22" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt22')"  onkeyup="javascript:return KeyUp(event,'txt22')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro1" class="box">
                          <asp:TextBox ID="txt03" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt03')"  onkeyup="javascript:return KeyUp(event,'txt03')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt04" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt04')"  onkeyup="javascript:return KeyUp(event,'txt04')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt05" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt05')"  onkeyup="javascript:return KeyUp(event,'txt05')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt13" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt13')"  onkeyup="javascript:return KeyUp(event,'txt13')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt14" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt14')"  onkeyup="javascript:return KeyUp(event,'txt14')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt15" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt15')"  onkeyup="javascript:return KeyUp(event,'txt15')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt23" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt23')"  onkeyup="javascript:return KeyUp(event,'txt23')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt24" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt24')"  onkeyup="javascript:return KeyUp(event,'txt24')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt25" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt25')"  onkeyup="javascript:return KeyUp(event,'txt25')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro2" class="box">
                          <asp:TextBox ID="txt06" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt06')"  onkeyup="javascript:return KeyUp(event,'txt06')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt07" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt07')"  onkeyup="javascript:return KeyUp(event,'txt07')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt08" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt08')"  onkeyup="javascript:return KeyUp(event,'txt08')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt16" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt16')"  onkeyup="javascript:return KeyUp(event,'txt16')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt17" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt17')"  onkeyup="javascript:return KeyUp(event,'txt17')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt18" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt18')"  onkeyup="javascript:return KeyUp(event,'txt18')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt26" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt26')"  onkeyup="javascript:return KeyUp(event,'txt26')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt27" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt27')"  onkeyup="javascript:return KeyUp(event,'txt27')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt28" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt28')"  onkeyup="javascript:return KeyUp(event,'txt28')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                      </div>

                      <!-- FILA 2 -->
                      <div id="recuadro3" class="box">
                          <asp:TextBox ID="txt30" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt30')"  onkeyup="javascript:return KeyUp(event,'txt30')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt31" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt31')"  onkeyup="javascript:return KeyUp(event,'txt31')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt32" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt32')"  onkeyup="javascript:return KeyUp(event,'txt32')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt40" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt40')"  onkeyup="javascript:return KeyUp(event,'txt40')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt41" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt41')"  onkeyup="javascript:return KeyUp(event,'txt41')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt42" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt42')"  onkeyup="javascript:return KeyUp(event,'txt42')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt50" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt50')"  onkeyup="javascript:return KeyUp(event,'txt50')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt51" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt51')"  onkeyup="javascript:return KeyUp(event,'txt51')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt52" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt52')"  onkeyup="javascript:return KeyUp(event,'txt52')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro4" class="box">
                          <asp:TextBox ID="txt33" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt33')"  onkeyup="javascript:return KeyUp(event,'txt33')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt34" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt34')"  onkeyup="javascript:return KeyUp(event,'txt34')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt35" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt35')"  onkeyup="javascript:return KeyUp(event,'txt35')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt43" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt43')"  onkeyup="javascript:return KeyUp(event,'txt43')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt44" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt44')"  onkeyup="javascript:return KeyUp(event,'txt44')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt45" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt45')"  onkeyup="javascript:return KeyUp(event,'txt45')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt53" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt53')"  onkeyup="javascript:return KeyUp(event,'txt53')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt54" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt54')"  onkeyup="javascript:return KeyUp(event,'txt54')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt55" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt55')"  onkeyup="javascript:return KeyUp(event,'txt55')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro5" class="box">
                          <asp:TextBox ID="txt36" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt36')"  onkeyup="javascript:return KeyUp(event,'txt36')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt37" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt37')"  onkeyup="javascript:return KeyUp(event,'txt37')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt38" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt38')"  onkeyup="javascript:return KeyUp(event,'txt38')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt46" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt46')"  onkeyup="javascript:return KeyUp(event,'txt46')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt47" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt47')"  onkeyup="javascript:return KeyUp(event,'txt47')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt48" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt48')"  onkeyup="javascript:return KeyUp(event,'txt48')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt56" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt56')"  onkeyup="javascript:return KeyUp(event,'txt56')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt57" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt57')"  onkeyup="javascript:return KeyUp(event,'txt57')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt58" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt58')"  onkeyup="javascript:return KeyUp(event,'txt58')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                      </div>

                      <!--  FILA 3-->
                      <div class="box">
                          <asp:TextBox ID="txt60" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt60')"  onkeyup="javascript:return KeyUp(event,'txt60')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt61" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt61')"  onkeyup="javascript:return KeyUp(event,'txt61')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt62" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt62')"  onkeyup="javascript:return KeyUp(event,'txt62')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt70" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt70')"  onkeyup="javascript:return KeyUp(event,'txt70')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt71" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt71')"  onkeyup="javascript:return KeyUp(event,'txt71')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt72" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt72')"  onkeyup="javascript:return KeyUp(event,'txt72')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt80" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt80')"  onkeyup="javascript:return KeyUp(event,'txt80')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt81" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt81')"  onkeyup="javascript:return KeyUp(event,'txt81')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt82" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt82')"  onkeyup="javascript:return KeyUp(event,'txt82')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div class="box">
                          <asp:TextBox ID="txt63" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt63')"  onkeyup="javascript:return KeyUp(event,'txt63')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt64" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt64')"  onkeyup="javascript:return KeyUp(event,'txt64')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt65" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt65')"  onkeyup="javascript:return KeyUp(event,'txt65')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt73" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt73')"  onkeyup="javascript:return KeyUp(event,'txt73')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt74" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt74')"  onkeyup="javascript:return KeyUp(event,'txt74')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt75" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt75')"  onkeyup="javascript:return KeyUp(event,'txt75')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt83" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt83')"  onkeyup="javascript:return KeyUp(event,'txt83')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt84" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt84')"  onkeyup="javascript:return KeyUp(event,'txt84')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt85" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt85')"  onkeyup="javascript:return KeyUp(event,'txt85')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                      </div>

                      <!--  -->
                      <div class="box">
                          <asp:TextBox ID="txt66" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt66')"  onkeyup="javascript:return KeyUp(event,'txt66')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt67" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt67')"  onkeyup="javascript:return KeyUp(event,'txt67')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt68" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt68')"  onkeyup="javascript:return KeyUp(event,'txt68')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt76" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt76')"  onkeyup="javascript:return KeyUp(event,'txt76')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt77" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt77')"  onkeyup="javascript:return KeyUp(event,'txt77')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt78" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt78')"  onkeyup="javascript:return KeyUp(event,'txt78')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                          <asp:TextBox ID="txt86" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt86')"  onkeyup="javascript:return KeyUp(event,'txt86')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt87" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt87')"  onkeyup="javascript:return KeyUp(event,'txt87')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox> <asp:TextBox ID="txt88" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress(event,'txt88')"  onkeyup="javascript:return KeyUp(event,'txt88')"   onclick="javascript:return DrawingMarket(this)" MaxLength="1"></asp:TextBox>
                      </div>
  
                   </div><!-- FIN INNERGRID -->
                  </div><!-- FIN GRID -->
                 </div><!-- CONTAINER2-->

             <div class="container" style="margin-right:16%; float:right;;width:500px;">
                    <div style="align-content:center">           
                     <label id="excluido" class="nameGrid2">Candidatos Individuales</label>
                    </div>
                <div class="grid">
                  <div class="innerGrid">

                      <!--FILA 1  -->
                      <div id="recuadro_0" class="box">
                          <asp:TextBox ID="txt_00" class="cTxt" runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_00')"  onkeyup="javascript:return KeyUp2(event,'txt_00')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_01" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_01')"  onkeyup="javascript:return KeyUp2(event,'txt_01')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_02" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_02')"  onkeyup="javascript:return KeyUp2(event,'txt_02')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_10" class="cTxt" runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_10')"  onkeyup="javascript:return KeyUp2(event,'txt_10')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_11" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_11')"  onkeyup="javascript:return KeyUp2(event,'txt_11')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_12" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_12')"  onkeyup="javascript:return KeyUp2(event,'txt_12')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_20" class="cTxt" runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_20')"  onkeyup="javascript:return KeyUp2(event,'txt_20')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_21" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_21')"  onkeyup="javascript:return KeyUp2(event,'txt_21')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_22" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_22')"  onkeyup="javascript:return KeyUp2(event,'txt_22')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro_1" class="box">
                          <asp:TextBox ID="txt_03" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_03')"  onkeyup="javascript:return KeyUp2(event,'txt_03')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_04" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_04')"  onkeyup="javascript:return KeyUp2(event,'txt_04')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_05" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_05')"  onkeyup="javascript:return KeyUp2(event,'txt_05')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_13" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_13')"  onkeyup="javascript:return KeyUp2(event,'txt_13')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_14" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_14')"  onkeyup="javascript:return KeyUp2(event,'txt_14')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_15" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_15')"  onkeyup="javascript:return KeyUp2(event,'txt_15')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_23" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_23')"  onkeyup="javascript:return KeyUp2(event,'txt_23')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_24" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_24')"  onkeyup="javascript:return KeyUp2(event,'txt_24')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_25" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_25')"  onkeyup="javascript:return KeyUp2(event,'txt_25')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro_2" class="box">
                          <asp:TextBox ID="txt_06" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_06')"  onkeyup="javascript:return KeyUp2(event,'txt_06')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_07" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_07')"  onkeyup="javascript:return KeyUp2(event,'txt_07')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_08" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_08')"  onkeyup="javascript:return KeyUp2(event,'txt_08')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_16" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_16')"  onkeyup="javascript:return KeyUp2(event,'txt_16')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_17" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_17')"  onkeyup="javascript:return KeyUp2(event,'txt_17')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_18" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_18')"  onkeyup="javascript:return KeyUp2(event,'txt_18')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_26" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_26')"  onkeyup="javascript:return KeyUp2(event,'txt_26')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_27" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_27')"  onkeyup="javascript:return KeyUp2(event,'txt_27')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_28" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_28')"  onkeyup="javascript:return KeyUp2(event,'txt_28')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                      </div>

                      <!-- FILA 2 -->
                      <div id="recuadro_3" class="box">
                          <asp:TextBox ID="txt_30" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_30')"  onkeyup="javascript:return KeyUp2(event,'txt_30')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_31" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_31')"  onkeyup="javascript:return KeyUp2(event,'txt_31')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_32" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_32')"  onkeyup="javascript:return KeyUp2(event,'txt_32')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_40" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_40')"  onkeyup="javascript:return KeyUp2(event,'txt_40')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_41" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_41')"  onkeyup="javascript:return KeyUp2(event,'txt_41')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_42" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_42')"  onkeyup="javascript:return KeyUp2(event,'txt_42')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_50" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_50')"  onkeyup="javascript:return KeyUp2(event,'txt_50')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_51" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_51')"  onkeyup="javascript:return KeyUp2(event,'txt_51')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_52" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_52')"  onkeyup="javascript:return KeyUp2(event,'txt_52')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro_4" class="box">
                          <asp:TextBox ID="txt_33" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_33')"  onkeyup="javascript:return KeyUp2(event,'txt_33')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_34" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_34')"  onkeyup="javascript:return KeyUp2(event,'txt_34')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_35" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_35')"  onkeyup="javascript:return KeyUp2(event,'txt_35')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_43" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_43')"  onkeyup="javascript:return KeyUp2(event,'txt_43')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_44" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_44')"  onkeyup="javascript:return KeyUp2(event,'txt_44')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_45" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_45')"  onkeyup="javascript:return KeyUp2(event,'txt_45')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_53" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_53')"  onkeyup="javascript:return KeyUp2(event,'txt_53')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_54" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_54')"  onkeyup="javascript:return KeyUp2(event,'txt_54')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_55" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_55')"  onkeyup="javascript:return KeyUp2(event,'txt_55')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                      </div>

                      <!--  -->
                      <div id="recuadro_5" class="box">
                          <asp:TextBox ID="txt_36" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_36')"  onkeyup="javascript:return KeyUp2(event,'txt_36')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_37" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_37')"  onkeyup="javascript:return KeyUp2(event,'txt_37')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_38" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_38')"  onkeyup="javascript:return KeyUp2(event,'txt_38')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_46" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_46')"  onkeyup="javascript:return KeyUp2(event,'txt_46')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_47" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_47')"  onkeyup="javascript:return KeyUp2(event,'txt_47')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_48" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_48')"  onkeyup="javascript:return KeyUp2(event,'txt_48')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_56" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_56')"  onkeyup="javascript:return KeyUp2(event,'txt_56')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_57" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_57')"  onkeyup="javascript:return KeyUp2(event,'txt_57')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_58" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_58')"  onkeyup="javascript:return KeyUp2(event,'txt_58')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                      </div>

                      <!--  FILA 3-->
                      <div class="box">
                          <asp:TextBox ID="txt_60" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_60')"  onkeyup="javascript:return KeyUp2(event,'txt_60')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_61" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_61')"  onkeyup="javascript:return KeyUp2(event,'txt_61')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_62" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_62')"  onkeyup="javascript:return KeyUp2(event,'txt_62')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_70" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_70')"  onkeyup="javascript:return KeyUp2(event,'txt_70')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_71" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_71')"  onkeyup="javascript:return KeyUp2(event,'txt_71')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_72" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_72')"  onkeyup="javascript:return KeyUp2(event,'txt_72')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_80" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_80')"  onkeyup="javascript:return KeyUp2(event,'txt_80')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_81" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_81')"  onkeyup="javascript:return KeyUp2(event,'txt_81')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_82" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_82')"  onkeyup="javascript:return KeyUp2(event,'txt_82')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                      </div>

                      <!--  -->
                      <div class="box">
                          <asp:TextBox ID="txt_63" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_63')"  onkeyup="javascript:return KeyUp2(event,'txt_63')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_64" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_64')"  onkeyup="javascript:return KeyUp2(event,'txt_64')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_65" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_65')"  onkeyup="javascript:return KeyUp2(event,'txt_65')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_73" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_73')"  onkeyup="javascript:return KeyUp2(event,'txt_73')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_74" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_74')"  onkeyup="javascript:return KeyUp2(event,'txt_74')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_75" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_75')"  onkeyup="javascript:return KeyUp2(event,'txt_75')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_83" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_83')"  onkeyup="javascript:return KeyUp2(event,'txt_83')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_84" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_84')"  onkeyup="javascript:return KeyUp2(event,'txt_84')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_85" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_85')"  onkeyup="javascript:return KeyUp2(event,'txt_85')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                      </div>

                      <!--  -->
                      <div class="box">
                          <asp:TextBox ID="txt_66" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_66')"  onkeyup="javascript:return KeyUp2(event,'txt_66')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_67" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_67')"  onkeyup="javascript:return KeyUp2(event,'txt_67')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_68" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_68')"  onkeyup="javascript:return KeyUp2(event,'txt_68')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_76" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_76')"  onkeyup="javascript:return KeyUp2(event,'txt_76')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_77" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_77')"  onkeyup="javascript:return KeyUp2(event,'txt_77')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_78" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_78')"  onkeyup="javascript:return KeyUp2(event,'txt_78')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                          <asp:TextBox ID="txt_86" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_86')"  onkeyup="javascript:return KeyUp2(event,'txt_86')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_87" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_87')"  onkeyup="javascript:return KeyUp2(event,'txt_87')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox> <asp:TextBox ID="txt_88" class="cTxt"  runat="server"  onkeypress="javascript:return KeyPress2(event,'txt_88')"  onkeyup="javascript:return KeyUp2(event,'txt_88')" onclick="javascript:return DrawingMarket(this)"  ></asp:TextBox>
                      </div>


                      </div><!-- FIN INNERGRID -->
                  </div><!-- FIN GRID -->
              </div><!-- CONTAINER2-->
    </div><!-- CONTAINER FULL-->
           <br /><br/>
             <div class="container body-content" style="margin-top:26%;">       
               <hr />
                 <footer>
                    <p>copyright&copy;  <%=DateTime.Now.Year %>   - Sudoku de Todos </p>
                 </footer>
            </div>

      </form>
    </body>
</html>