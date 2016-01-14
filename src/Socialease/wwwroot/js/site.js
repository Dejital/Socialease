// site.js
(function() {
    var ele = document.getElementById("username");
    ele.innerHTML = "Serge Nevsky";
    ele.onmouseenter = function() {
        ele.style["color"] = "green";
    };
    ele.onmouseleave = function() {
        ele.style["color"] = "";
    }
})();
