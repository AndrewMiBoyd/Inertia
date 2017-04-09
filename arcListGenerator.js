var getNextCells = function(list, position)
{
    if (position.i + position.j + Math.abs(position.k) >20)
        return;

    list.push({
        i: 1 + position.i,
        j: position.j,
        k: position.k
    });

    list.push({
        i: 2 + position.i,
        j: position.j,
        k: position.k
    });

    list.push({
        i: 1 + position.i,
        j: position.j + 1,
        k: position.k
    });

    list.push({
        i: 1 + position.i,
        j: position.j,
        k: position.k -1
    });

    getNextCells(list, {
        i: 1 + position.i,
        j: position.j + 1,
        k: position.k
    });
    getNextCells(list, {
        i: 1 + position.i,
        j: position.j,
        k: position.k -1
    });   

}
var generateCellList = function(position, rotation)
{
    var list = [];
    set = {};
    set.push = function(item)
    {
        this[JSON.stringify(item)] = true;
    }

    list.push({    
        i: position.i + 1,
        j: position.j,
        k: position.k })

    getNextCells(list, {
        i: position.i + 1,
        j: position.j,
        k: position.k})

    for (var x = 0; x < list.length; x++)
    {
        switch(rotation)
        {   
            case 1:
                var rotated = {};
                rotated.j = list[x].i;
                rotated.k = list[x].j;
                rotated.i = -list[x].k;
                set.push(rotated);
                break;
            case 2:
                var rotated = {};
                rotated.j = -list[x].k
                rotated.k = list[x].i;
                rotated.i = - list[x].j;
                set.push(rotated);
                break;
            case 3:
                var rotated = {};
                rotated.j = -list[x].k
                rotated.k = - list[x].j
                rotated.i = - list[x].i;
                set.push(rotated);
                break;
            case 4:
                var rotated = {};
                rotated.j = - list[x].i;
                rotated.k = - list[x].j;
                rotated.i =  list[x].k
                set.push(rotated);
                break;
            case 5:
                var rotated = {};
                rotated.j = list[x].k;
                rotated.k = - list[x].i;
                rotated.i = list[x].j
                set.push(rotated);
                break;
            default:
                set.push(list[x])
        } 
    }   
    console.log(set);
}
generateCellList({i:0, j:0, k:0}, 1);

