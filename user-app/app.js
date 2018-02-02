//app.js
App({
  onLaunch: function () {
    //调用API从本地缓存中获取数据
    var logs = wx.getStorageSync('logs') || []
    logs.unshift(Date.now())
    wx.setStorageSync('logs', logs)
    var that = this
    wx.login({
        success: function (data) {
            wx.request({
                url: 'https://api.weixin.qq.com/sns/jscode2session',
                method: 'GET',
                data: {
                    appid: 'wx58c73eb7df3f66c8',
                    secret: 'd9a5d17c4e41709cecc54651a1773e03',
                    js_code: data.code,
                    grant_type: 'authorization_code'
                },
                success: (data) => {
                    that.globalData.openID = data.data.openid;
                }
            })
            wx.getUserInfo({
                success: function (res) {
                    that.globalData.userInfo = res.userInfo
                    typeof cb == "function" && cb(that.globalData.userInfo)
                }
            });
        }
    })
  },
  globalData:{
    userInfo:null,
    openID:null,
    bloodID:null,
    guestBloodID:null,
    checkItem_1:'',
    checkItem_2:''
  }
})