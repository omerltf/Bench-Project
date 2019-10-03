//var map = new ol.Map({
//    target: 'map',
//    layers: [
//      new ol.layer.Tile({
//          source: new ol.source.OSM()
//      })
//    ],
//    view: new ol.View({
//        center: ol.proj.fromLonLat([37.41, 8.82]),
//        zoom: 4
//    })
//});
(async () => {
var baseMapLayer = new ol.layer.Tile({
    source: new ol.source.OSM()
});
var map = new ol.Map({
    target: 'map',
    layers: [baseMapLayer],
    view: new ol.View({
        center: ol.proj.fromLonLat([-74.0061, 40.712]),
        zoom: 7 //Initial Zoom Level
    })
});


//Adding a marker on the map
var marker = new ol.Feature({
    geometry: new ol.geom.Point(
      ol.proj.fromLonLat([-74.006, 40.7127])
    ),  // Cordinates of New York's Town Hall
});
var vectorSource = new ol.source.Vector({
    features: [marker]
});
var markerVectorLayer = new ol.layer.Vector({
    source: vectorSource,
});
map.addLayer(markerVectorLayer);


function addMarker (latitude, longitude){

    var point = new ol.geom.Point(
        ol.proj.fromLonLat([longitude, latitude])
      );
    
    var mark = new ol.Feature({     
        geometry: point,  // Cordinates of New York's Town Hall 
    });
    mark.benchId = 999;
    console.log('latitude is ' + latitude);
    console.log('longitude is '+ longitude);
    vectorSource.addFeature(mark);

}


async function getBenchList (){
    return await fetch ('api/Bench/Get');
}

async function drawMarkers (){
    let benches = await getBenchList();
    let jsonResponse = await benches.json();
    //console.log(jsonResponse)
    jsonResponse.benchInfo.forEach(element => {
        console.log(element)
        addMarker(element['Latitude'], element['Longitude']);
    });
}
drawMarkers();


function clickHandler (event){

    //debugger;
    map.forEachFeatureAtPixel(event.pixel, function (feature,layer){

        debugger;
        console.log('my feature is' + feature);
        var coord =  ol.proj.transform(event.coordinate, 'EPSG:3857', 'EPSG:4326');
        console.log (coord);
        console.log (feature.benchId);
    })


    // if (event.target instanceof ol.geom.Point){
    //     console.log('found point');
    // }
    console.log(event)
    //console.log(ol.geom.Point.getType());
    var coord =  ol.proj.transform(event.coordinate, 'EPSG:3857', 'EPSG:4326');
    var latitude = coord[1];
    var longitude = coord[0];
    //document.location.href = "/Bench/Create?latitude=" + latitude + "&longitude=" + longitude;
}

map.on('click', clickHandler);



})();