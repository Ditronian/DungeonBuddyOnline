function getEncounterName() {

    if (document.forms[0].BodyPlaceHolder_hiddenEncounterName.value !== null && document.forms[0].BodyPlaceHolder_hiddenEncounterName.value !== "") return true;

    var encounterPrompt = prompt("What is the name of this new encounter?");
    if (encounterPrompt !== null && encounterPrompt !== "") {
        document.forms[0].BodyPlaceHolder_hiddenEncounterName.value = encounterPrompt;
        return true;
    }
    else return false;
}

function getShopName() {

    if (document.forms[0].BodyPlaceHolder_hiddenShopName.value !== null && document.forms[0].BodyPlaceHolder_hiddenShopName.value !== "") return true;

    var shopPrompt = prompt("What is the name of this new shop?");
    if (shopPrompt !== null && shopPrompt !== "") {
        document.forms[0].BodyPlaceHolder_hiddenShopName.value = shopPrompt;
        return true;
    }
    else return false;
}

//This code taken and modified somewhat heavily from W3 Schools (https://www.w3schools.com/howto/howto_js_sort_table.asp)
function sortTable(column, desireAsc = false) {
    
    var table, rows, switching, i, x, y, dir, shouldSwitch, switchcount = 0;
    table = document.getElementById("BodyPlaceHolder_objectTable");
    switching = true;
    // Set the sorting direction to ascending:
    dir = "asc";
    /* Make a loop that will continue until
    no switching has been done: */
    while (switching) {
        // Start by saying: no switching is done:
        switching = false;
        rows = table.rows;
        /* Loop through all table rows (except the
        first, which contains table headers): */
        for (i = 1; i < rows.length - 1; i++) {
            // Start by saying there should be no switching:
            shouldSwitch = false;
            /* Get the two elements you want to compare,
            one from current row and one from the next: */
            x = rows[i].cells[column].children[0];
            y = rows[i + 1].cells[column].children[0];
            /* Check if the two rows should switch place,
            based on the direction, asc or desc: */
            if (dir === "asc" && isNaN(x.value)) {
                if (x.value.toLowerCase() < y.value.toLowerCase()) {
                    // If so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            } else if (dir === "desc" && isNaN(x.value)) {
                if (x.value.toLowerCase() > y.value.toLowerCase()) {
                    // If so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            }
            else if (dir === "asc" && !isNaN(x.value)) {
                if (parseInt(x.value, 10) > parseInt(y.value, 10)) {
                    // If so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            }
            else if (dir === "desc" && !isNaN(x.value)) {
                if (parseInt(x.value, 10) < parseInt(y.value, 10)) {
                    // If so, mark as a switch and break the loop:
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            /* If a switch has been marked, make the switch
            and mark that a switch has been done: */
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            // Each time a switch is done, increase this count by 1:
            switchcount++;
        } else {
            /* If no switching has been done AND the direction is "asc" AND "asc" isnt necessarily what I want,
            set the direction to "desc" and run the while loop again. */
            if (switchcount === 0 && dir === "asc" && desireAsc === false) {
                dir = "desc";
                switching = true;
            }
        }
    }
    
    document.forms[0].BodyPlaceHolder_hiddenSortColumn.value = column.toString();
    document.forms[0].BodyPlaceHolder_hiddenSortDir.value = dir;
    
    return false;  //Never postback.  Button is for sorting only.
}


//Get the button's row, then delete it from the table.
function deleteRow(elementID) {
    var row = document.getElementById(elementID).parentNode.parentNode.rowIndex;
    var table = document.getElementById("BodyPlaceHolder_objectTable");
    table.deleteRow(row);

    return false;
}


//Automatically sorts the encounter table based on the last sort, to preserve sorting preferences on postback.
function resort(defaultCol)
{
    var sortedColumn = document.forms[0].BodyPlaceHolder_hiddenSortColumn.value;
    var sortedDirection = document.forms[0].BodyPlaceHolder_hiddenSortDir.value;
    //Not previously sorted, default to sorting by initiative, descending
    if (sortedColumn === "")
    {
        sortTable(defaultCol, true); //asc
        sortTable(defaultCol); //desc
    }
    //Recall values, and protect ascending sorts in case they are already sorted.
    else
    {
        var sortedColumnInt = Number(document.forms[0].BodyPlaceHolder_hiddenSortColumn.value);

        if (sortedDirection === "asc") sortTable(sortedColumnInt, true); //asc
        else if (sortedDirection === "desc") {
            sortTable(sortedColumnInt, true); //asc
            sortTable(sortedColumnInt); //desc
        }
        
    }
}