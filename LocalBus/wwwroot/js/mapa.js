
function GetMap() {
    var map = new Microsoft.Maps.Map('#myMap', {credentials: 'AkclvbgfjdRI43pyQ7TM48Mx0Gz8LpXOJzYsm8-T3b7fFOt2n-GMm0USIPHg-YLu'});

    //Request the user's location
    navigator.geolocation.getCurrentPosition(function (position) {
        var loc = new Microsoft.Maps.Location(position.coords.latitude, position.coords.longitude);
        var loc2 = new Microsoft.Maps.Location(-22.488375, -48.559304);


        //Add a pushpin at the user's location.
        var pin = new Microsoft.Maps.Pushpin(loc);
        map.entities.push(pin);

        var pin2 = new Microsoft.Maps.Pushpin(loc2);
        map.entities.push(pin2);

        //Center the map on the user's location.
        map.setView({ center: loc, zoom: 15 });
    });
}



