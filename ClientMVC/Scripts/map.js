var map;
var marker = false;
var previousLocation = {};
var mapCenter = {
    lat: 57.006617,
    lng: 9.879724
};

function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: mapCenter.lat, lng: mapCenter.lng },
        zoom: 8
    });
}

function initMarkerMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: mapCenter.lat, lng: mapCenter.lng },
        zoom: 8
    });

    //create or update the marker through click and drag events
    google.maps.event.addListener(map, 'click', function (event) {
        var clickedLocation = event.latLng;

        if (marker) {
            previousLocation.lat = marker.position.lat();
            previousLocation.lng = marker.position.lng();
        }

        if (!marker) {
            marker = new google.maps.Marker({
                position: clickedLocation,
                map: map,
                draggable: true
            });

            google.maps.event.addListener(marker, 'dragend', function (event) {
                markerLocation();
            });
        } else {
            marker.setPosition(clickedLocation);
        }

        markerLocation();
        if (previousLocation.lat) {
            showDistanceToPrevious();
        }
    });
}

//update marker text fields
function markerLocation() {
    var currentLocation = marker.getPosition();

    if (document.getElementById('marker-lat') && document.getElementById('marker-lng')) {
        document.getElementById('marker-lat').value = currentLocation.lat();
        document.getElementById('marker-lng').value = currentLocation.lng();
    }
}

function getCoordinates() {
    var lng;
    var lat;

    var lngInput = document.getElementById('lng');
    var latInput = document.getElementById('lat');
    lng = marker.position.lng();
    lat = marker.position.lat();
    lngInput.value = lng;
    latInput.value = lat;
    }