var host = {
    'dev':'https://wx.t.17u.cn',
    'product':'https://wx.17u.cn',
    'qa':'https://wx.qa.17u.cn'
}
var url = {
    'IsValidSessionID':'/health/ajax/userapp/IsValidSessionID',
    'saveBaseInfo':'/health/ajax/userapp/saveBaseInfo',
    'SubmitQuestionSurvey':'/health/ajax/userapp/SubmitQuestionSurvey',
    'GetGuestInfo':'/health/ajax/userapp/GetGuestInfo',
    'demo':'/ivacation/ajaxhelper/GetDepartCityList'
}
module.exports = {
    host,
    url
}