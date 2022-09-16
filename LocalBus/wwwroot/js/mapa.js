function GetMap() {


    var map = new Microsoft.Maps.Map('#myMap',
        {
            center: new Microsoft.Maps.Location(-22.489303078308875, -48.54630861383559),
            zoom: 13

        });

    map.setView({
        mapTypeId: Microsoft.Maps.MapTypeId.aerial
    });
    var num = 3

    var loc2 = new Microsoft.Maps.Location(-22.489303078308875, -48.54630861383559)
    var pin2 = new Microsoft.Maps.Pushpin(loc2, {
        title: "ETEC",
        subTitle: "João Rayz"
    });
    map.entities.push(pin2);
}