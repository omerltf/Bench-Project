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

var vectorSource = new ol.source.Vector({
    features: [new ol.Feature()]
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
        geometry: point, 
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
let jsonResponse = await benches.json();

async function drawMarkers (response){
    vectorSource.clear();
    response.forEach(element => {
        console.log(element)
        addMarker(element['Latitude'], element['Longitude'], element['Id']);
    });
}
drawMarkers(jsonResponse.benchList);


function clickHandler (event){
    let flag =true;
    map.forEachFeatureAtPixel(event.pixel, function (feature,layer){

        flag = false;
        document.location.href = "/Bench/Details/" + feature.getId();
    })

    if (flag == true){
    var coord =  ol.proj.transform(event.coordinate, 'EPSG:3857', 'EPSG:4326');
    var latitude = coord[1];
    var longitude = coord[0];
    document.location.href = "/Bench/Create?latitude=" + latitude + "&longitude=" + longitude;}
}

map.on('click', clickHandler);

document.getElementById("minInput").addEventListener("keyup", minFunction)
document.getElementById("maxInput").addEventListener("keyup", maxFunction)

let filteredbench = [];
filteredbench = jsonResponse.benchList;
DisplayBenches(filteredbench)
let minvalue = undefined;
let maxvalue = undefined;
function minFunction (event) {
    minvalue = document.getElementById("minInput").value
    console.log('min value is' + minvalue)
    console.log('max value is'+ maxvalue)
    if (!(minvalue==0 || minvalue==undefined)){
        if (maxvalue==undefined || maxvalue==0){
            filteredbench = [];
            for(let bench of jsonResponse.benchList){
                if (bench['NumberOfSeats']>=minvalue){
                    filteredbench.push(bench);
                    console.log(bench['NumberOfSeats'])
                }
            }
        }
        else {
            filteredbench = [];
            for(let bench of jsonResponse.benchList){
                if (bench['NumberOfSeats']<=maxvalue && bench['NumberOfSeats']>=minvalue){
                    filteredbench.push(bench);
                    console.log(bench['NumberOfSeats'])
                }
            }
        }
    }
    else {
        filteredbench = jsonResponse.benchList;
    }
    vectorSource.clear();
    drawMarkers(filteredbench);
    DisplayBenches(filteredbench)
}

function maxFunction (event) {
    
    maxvalue = document.getElementById('maxInput').value
    console.log('min value is' + minvalue)
    console.log('max value is'+ maxvalue)
    if (!(maxvalue==0 || maxvalue==undefined)){
        if (minvalue==undefined || minvalue==0){
            filteredbench = [];
            for(let bench of jsonResponse.benchList){
                if (bench['NumberOfSeats']<=maxvalue){
                    filteredbench.push(bench);
                    console.log(bench['NumberOfSeats'])
                }
            }
        }
        else {
            filteredbench = [];
            for(let bench of jsonResponse.benchList){
                if (bench['NumberOfSeats']<=maxvalue && bench['NumberOfSeats']>=minvalue){
                    filteredbench.push(bench);
                    console.log(bench['NumberOfSeats'])
                }
            }
        }
    }
    else {
        filteredbench = jsonResponse.benchList;    
    }
    vectorSource.clear();
    drawMarkers(filteredbench);
    DisplayBenches(filteredbench)
}


function DisplayBenches (filteredBenches){
    var table = document.getElementById("table")
    table.innerHTML = "";
    addHead(table,'Description')
    addHead(table,'NumberOfSeats')
    addHead(table,'CreatorUserId')
    addHead(table,'Name')
    addHead(table,'Latitude')
    addHead(table,'Longitude')
    addHead(table,'Rating')
    addHead(table,'NumberOfReviews')
    addHead(table,'')
    for (let bench of filteredBenches){
        console.log(bench);
        let rating = bench.Rating;
        if (rating == 0 ) {rating = 'No Rating'}
        addRow(
            table,
            bench.Description,
            bench.NumberOfSeats,
            bench.CreatorUserId,
            bench.Name,
            bench.Latitude,
            bench.Longitude,
            rating,
            bench.NumberOfReviews,
            `<a href="/Bench/Details/${bench.Id}">Details</a> `
            )
    }
}

function addHead(tr, val){
    var th = document.createElement('th');
    th.innerHTML = val;
    tr.appendChild(th)
}

function addCell(tr, val) {
    var td = document.createElement('td');

    td.innerHTML = val;

    tr.appendChild(td)
}

function addRow(tbl, val_1, val_2, val_3, val_4, val_5, val_6, val_7, val_8, val_9) {
    var tr = document.createElement('tr');

    addCell(tr, val_1);
    addCell(tr, val_2);
    addCell(tr, val_3);
    addCell(tr, val_4);
    addCell(tr, val_5);
    addCell(tr, val_6);
    addCell(tr, val_7);
    addCell(tr, val_8);
    addCell(tr, val_9);

    tbl.appendChild(tr)
}
})();