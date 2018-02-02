const requestAjax = require('../../config/request.js');
const requestUrl = require('../../config/url.js');
const regex = require('../../config/regex.js');

Page({
    data:{
        scancode:'',
        checkType_1:'',
        checkType_2:'',
        baseInfo:{
            bloodID:'',
            barCode:'',
            guestName:'',
            height:'',
            phoneNumber:'',
            gender:'',
            age:'',
            weight:'',
            familyHistoryOfCancer:'',
            medicalHistory:'',
            checkItem:'',
            openID:'',
            statusCode:'',
            guestBloodID:0,
        },
        checkData:{
            guestName:'',
            age:'',
            height:'',
            weight:'',
            phoneNumber:'',
            medicalHistory:'',
            checkItem:''
        },
        bloodID:'',
        barCode:'',
        guestName:'',
        height:'',
        phoneNumber:'',
        gender:'1',
        age:'',
        weight:'',
        familyHistoryOfCancer:'',
        medicalHistory:'',
        checkItem:'',
        openID:'',
        statusCode:'',
        guestBloodID:'',
        checkInfos:''
    },
    onLoad(query){
        if(query && query.scancode){
            this.setData({
                scancode:query.scancode,
                checkType_1:getApp().globalData.checkItem_1,
                checkType_2:getApp().globalData.checkItem_2,
            })
        }
        if(query && query.back){
            wx.request({
                url:requestUrl.host.dev + requestUrl.url.GetGuestInfo,
                methods:"GET",
                data:{
                    barCode:"",
                    openID:getApp().globalData.openID
                },
                success:(data)=>{
                    console.log(data);
                    var result = data.data.GuestInfo;
                    getApp().globalData.bloodID = result.BloodID;
                    this.setData({
                        scancode:result.BarCode,
                        bloodID:result.BloodID,
                        barCode:result.BarCode,
                        guestName:result.GuestName,
                        height:result.Height,
                        phoneNumber:result.PhoneNumber,
                        gender:result.Gender,
                        age:result.Age,
                        weight:result.Weight,
                        familyHistoryOfCancer:result.FamilyHistoryOfCancer,
                        medicalHistory:result.MedicalHistory,
                        checkItem:result.CheckItem,
                        checkItem_1:result.CheckItem.split(',')[0],
                        checkItem_2:result.CheckItem.split(',')[1]||'',
                        statusCode:result.StatusCode,
                        guestBloodID:result.GuestBloodID,
                        isAnswerQuestion:result.IsAnswerQuestion
                    });
                }
            })
        }
    },
    bindPickerChange(e){
        this.setData({
            index:e.detail.value
        })
    },
    bindInfoSub(){
        this.setData({
            baseInfo:{
                bloodID:getApp().globalData.bloodID,
                barCode:this.data.scancode,
                guestName:this.data.guestName,
                height:this.data.height,
                phoneNumber:this.data.phoneNumber,
                gender:this.data.gender,
                age:this.data.age,
                weight:this.data.weight,
                familyHistoryOfCancer:this.data.familyHistoryOfCancer,
                medicalHistory:this.data.medicalHistory,
                checkItem:this.data.checkItem,
                openID:getApp().globalData.openID,
                statusCode:0,
                guestBloodID:this.data.guestBloodID||0
            },
            checkData: {
                guestName: this.data.guestName,
                age: this.data.age,
                height: this.data.height,
                weight: this.data.weight,
                phoneNumber: this.data.phoneNumber,
                checkItem: this.data.checkItem
            },
        });
        if(this.data.checkInfos !== ''){
            if(regex.isNull(this.data.checkData)){
                wx.request({
                    url:requestUrl.host.dev + requestUrl.url.saveBaseInfo,
                    method:'POST',
                    data:{
                        baseInfo:this.data.baseInfo
                    },
                    success:(data)=>{
                        var result = data.data;
                        if(result.IsSuccess){
                            getApp().globalData.guestBloodID = result.GuestBloodID;
                            if(!this.data.isAnswerQuestion || this.data.isAnswerQuestion === ''){
                                wx.showModal({
                                    title: '提交成功',
                                    content: '去做一份调查问卷帮助医生更了解你的身体吧！',
                                    showCancel: true,
                                    success: (res) => {
                                        if (res.confirm) {
                                            wx.redirectTo({
                                                url: '../../pages/questions/index'
                                            })
                                        }
                                        if(res.cancel){
                                            wx.redirectTo({
                                                url: '../../pages/success/index'
                                            })
                                        }
                                    }
                                })
                            }
                        }else{
                            wx.showModal({
                                title: '提交失败',
                                content: result.Message,
                                showCancel: true,
                                success: (res) => {
                                }
                            })
                        }
                    }
                })
            }else{
                wx.showToast({
                    title: '请填写完整信息',
                    success: () => {
                        setTimeout(function () {
                            wx.hideToast()
                        }, 2000)
                    }
                });
            }
        }else{
            wx.showToast({
                title: '请阅读并同意知情同意书',
                success: () => {
                    setTimeout(function () {
                        wx.hideToast()
                    }, 2000)
                }
            });
        }
    },
    bindguestName(e){
        this.setData({
            guestName:e.detail.value
        })
    },
    bindage(e){
        this.setData({
            age:e.detail.value
        })
    },
    bindheight(e){
        this.setData({
            height:e.detail.value
        })
    },
    bindweight(e){
        this.setData({
            weight:e.detail.value
        })
    },
    bindphoneNumber(e){
        if (regex.phone(e.detail.value)){
            this.setData({
                phoneNumber: e.detail.value
            })
        }else{
            wx.showToast({
                title: '请输入有效手机号码',
                success: () => {
                    setTimeout(function () {
                        wx.hideToast()
                    }, 2000)
                }
            });
        }
        
    },
    bindmedicalHistory(e){
        this.setData({
            medicalHistory:e.detail.value
        })
    },
    bindfamilyHistoryOfCancer(e){
        this.setData({
            familyHistoryOfCancer:e.detail.value
        })
    },
    radioChange(e){
        this.setData({
            gender:e.detail.value
        })
    },
    checkboxChange(e){
        this.setData({
            checkItem:e.detail.value
        })
    },
    bindInfos(){
        wx.navigateTo({
            url: '../../pages/agreeInfo/index',
        })
    },
    bindCheckInfos(e){
        var data = e.detail.value;
        this.setData({
            checkInfos:data.length !== 0 ?　data[0] : ''
        })
    }
})