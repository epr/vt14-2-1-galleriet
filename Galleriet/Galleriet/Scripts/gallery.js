var b = document.getElementById("CloseMessage"),
    m = document.getElementById("SuccessMessage");
console.log(b);
if (b) {
    console.log("yes");
    b.addEventListener("click", function (e) {
        e.preventDefault();
        b.parentElement.removeChild(b);
        m.parentElement.removeChild(m);
    });
}