	var navegador = navigator.appName 
	switch (navegador) {
		case "Konqueror", "Opera", "Netscape" :
			document.write("<link href=../estilos/navegador_outros.css rel=stylesheet type=text/css>");
			document.write("<link href=../estilos/Layout_outros.css rel=stylesheet type=text/css>");
			break;	  
	  	default:
			document.write("<link href=../estilos/navegador_ie.css rel=stylesheet type=text/css>");    
			document.write("<link href=../estilos/Layout_ie.css rel=stylesheet type=text/css>");    
	}