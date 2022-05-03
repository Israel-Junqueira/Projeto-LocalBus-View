
function GetMap() {
    var map = new Microsoft.Maps.Map('#myMap', {credentials: 'AkclvbgfjdRI43pyQ7TM48Mx0Gz8LpXOJzYsm8-T3b7fFOt2n-GMm0USIPHg-YLu'});




        var loc2 = new Microsoft.Maps.Location(-22.488375, -48.559304);


        var pin2 = new Microsoft.Maps.Pushpin(loc2);
        map.entities.push(pin2);

        //Center the map on the user's location.
        map.setView({ center: loc, zoom: 15 });

}

function getcoordenadas(lat, long) {

    var lat2 =parseFloat( lat)
    var long2 =parseFloat( long)

    var teste = lat2 + long2
    console.log (teste)


    var loc3 = new Microsoft.Maps.Location(lat2, long2)
    var pin3 = new Microsoft.Maps.Pushpin(loc3);
    map.entities.push(pin3);


}