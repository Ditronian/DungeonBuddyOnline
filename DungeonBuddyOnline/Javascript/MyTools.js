function carrotFlipper(string) {

    if (string.includes("fa fa-caret-down")) return string.replace("fa fa-caret-down", "fa fa-caret-up");
    else if (string.includes("fa fa-caret-up")) return string.replace("fa fa-caret-up", "fa fa-caret-down");
    else alert(string);
}






