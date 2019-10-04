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


function addMarker (latitude, longitude, benchId){

    var point = new ol.geom.Point(
        ol.proj.fromLonLat([longitude, latitude])
      );
    
    var mark = new ol.Feature({     
        geometry: point,  // Cordinates of New York's Town Hall 
    });
    mark.setId(benchId);
    console.log('latitude is ' + latitude);
    console.log('longitude is '+ longitude);
    vectorSource.addFeature(mark);

}


async function getBenchList (){
    return await fetch ('api/Bench/Get');
}
let benches = await getBenchList();

async function drawMarkers (){
    let jsonResponse = await benches.json();
    //console.log(jsonResponse)
    jsonResponse.benchList.forEach(element => {
        //console.log(element)
        addMarker(element['Latitude'], element['Longitude'], element['benchId']);
    });
}
drawMarkers();


function clickHandler (event){
    let flag =true;
    //debugger;
    map.forEachFeatureAtPixel(event.pixel, function (feature,layer){

        //debugger;
        flag = false;
        //console.log('my feature is' + feature);
        // var coord =  ol.proj.transform(event.coordinate, 'EPSG:3857', 'EPSG:4326');
        // console.log (coord);
        //console.log ('my Id is: ' + feature.getId());
        document.location.href = "/Bench/Details/" + feature.getId();
    })


    // if (event.target instanceof ol.geom.Point){
    //     console.log('found point');
    // }
    //console.log(event)
    //console.log(ol.geom.Point.getType());
    if (flag == true){
    var coord =  ol.proj.transform(event.coordinate, 'EPSG:3857', 'EPSG:4326');
    var latitude = coord[1];
    var longitude = coord[0];
    document.location.href = "/Bench/Create?latitude=" + latitude + "&longitude=" + longitude;}
}

map.on('click', clickHandler);

document.getElementById("minInput").addEventListener("keyup", minFunction)
document.getElementById("maxInput").addEventListener("keyup", maxFunction)

function minFunction (event) {
    let input = document.getElementById('minInput');
    let list = document.getElementsByClassName('inner-list');
    console.log(list);
    for (let item of list){
        console.log(item);
        //let num = item.getElementById('Number');
        //console.log (num);
    }
}

function maxFunction (event) {
    let input = document.getElementById('maxInput');
    let list = document.getElementsByClassName('inner-list');
    list.forEach(element => {
        let num = element.getElementById('Number');
        console.log(num);
    });
}


function DisplayBenches (filteredBenches){
    var table = document.getElementById("table")
    for (let bench of filteredBenches){
        console.log(bench);
        //addRow(table, )
    }
}

function addCell(tr, val) {
    var td = document.createElement('td');

    td.innerHTML = val;

    tr.appendChild(td)
}

function addRow(tbl, val_1, val_2, val_3) {
    var tr = document.createElement('tr');

    addCell(tr, val_1);
    addCell(tr, val_2);
    addCell(tr, val_3);

    tbl.appendChild(tr)
}



})();