var getNextCells = function(list, position)
{
    if (position.i + position.j + Math.abs(position.k) >4)
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
var generateCellList = function(position)
{
    var list = {};
    list.push = function(item)
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


    console.log(list);
}
generateCellList({i:0, j:0, k:0});

