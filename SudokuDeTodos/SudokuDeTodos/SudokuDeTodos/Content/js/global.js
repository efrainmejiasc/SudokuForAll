



function MenuStick() {
    const style = document.getElementById("sidebar").style.display;
    if (style == 'block') {
        document.getElementById("sidebar").style.width = "0";
        document.getElementById("contenido").style.marginLeft = "0";
        document.getElementById("sidebar").style.display='none';
    }
    else if (style == 'none') {
        document.getElementById("sidebar").style.width = "300px";
        document.getElementById("contenido").style.marginLeft = "300px";
        document.getElementById("sidebar").style.display = 'block';
    }
    
}