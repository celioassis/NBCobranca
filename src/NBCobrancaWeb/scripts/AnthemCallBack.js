function Anthem_PreCallBack() {
	var loading = document.createElement("div");
	loading.id = "loading";
	loading.style.color = "black";
	loading.style.backgroundColor = "red";
	loading.style.paddingLeft = "5px";
	loading.style.paddingRight = "5px";
	loading.style.position = "absolute";
	loading.style.right = "10px";
	loading.style.top = "10px";
	loading.style.zIndex = "9999";
	loading.innerHTML = "Carregando...";
	document.body.appendChild(loading);
}
function Anthem_PostCallBack() {
	var loading = document.getElementById("loading");
	document.body.removeChild(loading);
}

