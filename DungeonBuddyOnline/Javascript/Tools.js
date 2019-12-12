function getModifier(score)
{
    if (score === 0 || score === 1) return -5;
    else if (score === 2 || score === 3) return -4;
    else if (score === 4 || score === 5) return -3;
    else if (score === 6 || score === 7) return -2;
    else if (score === 8 || score === 9) return -1;
    else if (score === 10 || score === 11) return 0;
    else if (score === 12 || score === 13) return 1;
    else if (score === 14 || score === 15) return 2;
    else if (score === 16 || score === 17) return 3;
    else if (score === 18 || score === 19) return 4;
    else if (score === 20 || score === 21) return 5;
    else if (score === 22 || score === 23) return 6;
    else if (score === 24 || score === 25) return 7;
    else return 8;
}

//Translates an integer value into the String equivalent for a given sided dice.
function getDice(num)   
{
    var str = "";
    switch (num) {
        case 4: str = "d4"; break;
        case 6: str = "d6"; break;
        case 8: str = "d8"; break;
        case 10: str = "d10"; break;
        case 12: str = "d12"; break;
        case 20: str = "d20"; break;
    }
    return str;
}

// Roll 20 sided dice method/overloaded methods
function d20()
{
    return Math.floor(Math.random() * 20)+1;
}

function d20(modifier) 
{
    return Math.floor(Math.random() * 20) + 1+modifier;
}

function d20(modifier, diceNumber) 
{
    var total = 0;
    for (i = 0; i < diceNumber; i++)
    {
        total += Math.floor(Math.random() * 20)+1;
    }
    return total + modifier;
}

// Roll 12 sided dice method/overloaded methods
function d12()
{
    return Math.floor(Math.random() * 12) + 1;
}

function d12(modifier)
{
    return Math.floor(Math.random() * 12) + 1 + modifier;
}

function d12(modifier, diceNumber)
{
    var total = 0;
    for (i = 0; i < diceNumber; i++)
    {
        total += Math.floor(Math.random() * 12) + 1;
    }
    return total + modifier;
}

// Roll 10 sided dice method/overloaded methods
function d10()
{
    return Math.floor(Math.random() * 10) + 1;
}

function d10(modifier)     
{
    return Math.floor(Math.random() * 10) + 1 + modifier;
}

function d10(modifier, diceNumber)     
{
    var total = 0;
    for (i = 0; i < diceNumber; i++)
    {
        total += Math.floor(Math.random() * 10) + 1;
    }
    return total + modifier;
}

// Roll 8 sided dice method/overloaded methods
function d8()
{
    return Math.floor(Math.random() * 8) + 1;
}

function d8(modifier)      
{
    return Math.floor(Math.random() * 8) + 1 + modifier;
}

function d8(modifier, diceNumber)      
{
    var total = 0;
    for (i = 0; i < diceNumber; i++)
    {
        total += Math.floor(Math.random() * 8) + 1;
    }
    return total + modifier;
}

// Roll 6 sided dice method/overloaded methods
function d6()
{
    return Math.floor(Math.random() * 6) + 1;
}

function d6(modifier)      
{
    return Math.floor(Math.random() * 6) + 1 + modifier;
}

function d6(modifier, diceNumber)      
{
    var total = 0;
    for (i = 0; i < diceNumber; i++)
    {
        total += Math.floor(Math.random() * 6) + 1;
    }
    return total + modifier;
}

// Roll 4 sided dice method/overloaded methods
function d4()
{
    return Math.floor(Math.random() * 4) + 1;
}

function d4(modifier)      
{
    return Math.floor(Math.random() * 4) + 1 + modifier;
}

function d4(modifier, diceNumber)      
{
    var total = 0;
    for (i = 0; i < diceNumber; i++)
    {
        total += Math.floor(Math.random() * 4) + 1;
    }
    return total + modifier;
}