<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>BackOffice</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="../../favicon.ico">
    <!---- Page Style Sheet ---->
    
    

 <link href="../dash/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />

    <!---- Page javaScript ---->
       <link href="../css/Newstyle.css" rel="stylesheet" />
    
</head>
<body class="img js-fullheight" style="background-image: url(images/bg.jpg);">
    <form id="form1" runat="server">
        
       

        <section class="ftco-section">
		<div class="container">
			<div class="row justify-content-center">
				<div class="col-md-6 text-center mb-5">
					<img runat="server" src="../images/logo1.png"  onkeypress="" title="logo" alt="logo" style="width:200px;background-color: #00000087;
    border-radius: 117px;
    border: 1px solid #fff;" />

				</div>
			</div>
			<div class="row justify-content-center">
				<div class="col-md-6 col-lg-4">
					<div class="login-wrap p-0">
		      	<h3 class="mb-4 text-center">Have an account?</h3>
		      	
		      		<div class="form-group">
                            <asp:TextBox ID="txtUserId" CssClass="form-control" placeholder="Enter your email" oncopy="return false" TabIndex="1" runat="server" Text=""></asp:TextBox>
		      		
		      		</div>
	            <div class="form-group">
	              <asp:TextBox ID="txtPwd" CssClass="form-control" placeholder="Enter Password" runat="server" TabIndex="2" TextMode="Password" Text=""></asp:TextBox>
	              <span toggle="#password-field" class="fa fa-fw fa-eye field-icon toggle-password"></span>
	            </div>
	            <div class="form-group">
	            	
                       <asp:Button ID="Button1" TabIndex="3" CssClass="form-control btn btn-primary submit px-3 " Text="Sign In" runat="server" ValidationGroup="a" OnClick="btnSubmit_Click" />
                                            <%--  <asp:ImageButton ID="imgbtnSubmit" AlternateText="Submit" TabIndex="3" ImageUrl="~/Images/loginbtn.jpg"
                                                runat="server" ValidationGroup="a" OnClick="imgbtnSubmit_Click" />--%>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="a"
                                                ShowMessageBox="true" ShowSummary="false" />
	            </div>
	         
                      <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
	        
	         
		      </div>
				</div>
			</div>
		</div>
	</section>






























        <%--<div class="container " style="margin-top: 5px;">
            <!---- Header End ---->
            <div class="row" style="height: 50px;">
            </div>
            <div class="row">
                <div class="col-md-7">
                </div>
                <div class="col-md-5">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-form">
                                
                                <div class="panel-body">
                                    <form class="panel panel-form">
                                        <div class="form-group">
                                            <div class="text-center mb-50"> <img runat="server" src="~/images/logo1.png"  onkeypress="" title="logo" alt="logo" /></div>
                                          <h4 class="text-center">Sign in </h4>
                                            <div class="has-feedback">
                                                <label class="contrl-lable" for="exampleInputEmail1">
                                                    USER ID  
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Invalid User Name"
                                                        ControlToValidate="txtUserId" Font-Size="20px" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                                </label>
                                                <asp:TextBox ID="txtUserId" CssClass="form-control" placeholder="Enter your email" oncopy="return false" TabIndex="1" runat="server" Text=""></asp:TextBox>
                                                <i class="fa fa-user form-control-feedback txt-grey"></i>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="has-feedback">
                                                <label class="contrl-lable" for="exampleInputPassword1">
                                                    PASSWORD 
                                                    <asp:RequiredFieldValidator Font-Size="20px" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Invalid password"
                                                        ControlToValidate="txtPwd" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                                </label>
                                                <asp:TextBox ID="txtPwd" CssClass="form-control" placeholder="Enter Password" runat="server" TabIndex="2" TextMode="Password" Text=""></asp:TextBox>
                                                <i class="fa fa-lock form-control-feedback txt-grey"></i>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="has-feedback">
                                                <asp:Label ID="lblMsg" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="form-group mb-30">
                                            <asp:Button ID="btnSubmit" TabIndex="3" CssClass="btn btn-primary btn-lg btn-block " Text="Login" runat="server" ValidationGroup="a" OnClick="btnSubmit_Click" />
                                           
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="a"
                                                ShowMessageBox="true" ShowSummary="false" />
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                    </div>
                </div>
                
            </div>
            <div class="row" style="height: 50px;">
            </div>
        </div></section>
        <div class="footer_logo">
            <div class="container">
                <div class="row">
                    <div class="col-md-12">
                        <img runat="server" src="~/images/footlogo.jpg" class="img-responsive" />
                    </div>
                </div>
            </div>
        </div>--%>
        <!---- Footer Start ---->

        <!---- Footer End ---->
        <div class="clearfix">
        </div>
    </form>
</body>
</html>
