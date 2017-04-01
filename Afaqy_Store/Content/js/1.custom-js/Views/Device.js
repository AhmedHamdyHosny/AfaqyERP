function getDeviceFilterData(imei) {
    var data = { Options: { Filters: [{ Property: 'IMEI', Operation: 'Equal', Value: imei }] } };
    return data;
}