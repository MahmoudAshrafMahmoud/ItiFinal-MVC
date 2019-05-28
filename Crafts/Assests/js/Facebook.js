

var postperprofilecounter=0;
function Register(event){
    var password=document.getElementById("psw");
    var conf_password=document.getElementById("psw-repeat");
    if(password.value != conf_password.value) {
        event.preventDefault();}
    var user={};
    user.name = document.getElementById("Username").value;
    user.email = document.getElementById("email").value;
    user.password= document.getElementById("psw").value;
    user.profileImg=document.getElementById("profileImg").value;
    user.coverImge=document.getElementById("coverImge").value;

    if(JSON.parse(localStorage.getItem('usersArray'))!=null)
        {
        var a = [];
        a=JSON.parse(localStorage.getItem('usersArray'));
        a.push(user);
        localStorage.setItem('usersArray', JSON.stringify(a));
        console.log(JSON.parse(localStorage.getItem('usersArray')));
        }
   else
        {
            localStorage.setItem('usersArray', JSON.stringify(Users));
            var a = [];
            a=JSON.parse(localStorage.getItem('usersArray'));
            a.push(user);
            localStorage.setItem('usersArray', JSON.stringify(a));
            console.log(JSON.parse(localStorage.getItem('usersArray')));
        }
    }





      //Create stoarge of users

      //Log in function 


      function SharePost(event)
      {    event.preventDefault();
        postperprofilecounter=postperprofilecounter+1;
              var content = document.getElementById("NewpostConent").value;
              var NewPost = document.createElement("div");
              NewPost.id="post"+postperprofilecounter;
              NewPost.classList.add("post");
              var posts = document.getElementById("Myposts");
              posts.appendChild(NewPost);
              var Postowner = document.createElement("h3");
              NewPost.appendChild(Postowner);
              var PostownerImg = document.createElement("img");
              PostownerImg.classList.add("mypostuserimage");
              PostownerImg.src = localStorage.getItem('CurrentUserPImge');
              Postowner.appendChild(PostownerImg);
              var PostownerName = document.createElement("span");            
              PostownerName.classList.add("postusername");
              PostownerName.innerText= localStorage.getItem('CurrentUserName');
              Postowner.appendChild(PostownerName);
              var newcontentdiv= document.createElement("div");
              newcontentdiv.classList.add("MypostContent");
              NewPost.appendChild(newcontentdiv);
              var newcontent= document.createElement("p");
              newcontent.id="PostContent"+postperprofilecounter;
              newcontent.innerText=content;
              newcontentdiv.appendChild(newcontent);

              //add comment text area
              var NewCommentTextAreaDiv = document.createElement("div");
              NewCommentTextAreaDiv.classList.add("newComment");
              NewPost.appendChild(NewCommentTextAreaDiv);

              var NewCommentform = document.createElement("form");
              NewCommentTextAreaDiv.appendChild(NewCommentform);

              var NewCommenttextarea = document.createElement("textarea");
              NewCommenttextarea.classList.add("NewCommenttextarea");
              NewCommenttextarea.id="NewCommentContent"+postperprofilecounter;
              NewCommenttextarea.placeholder="write your comment...";
              // NewCommenttextarea.onkeydown+=auto_grow(this);
              // NewCommenttextarea.addEventListener("keydown", auto_grow(this));

              NewCommentform.appendChild(NewCommenttextarea);

              var NewCommentbutton = document.createElement("button");
              NewCommentbutton.classList.add("commentbuttonstyle");
              NewCommentbutton.id="Commentsubmit"+postperprofilecounter;
              // NewCommentbutton.onclick+=comment(this);
              NewCommentbutton.addEventListener("click", comment,false)
              NewCommentbutton.myParam = postperprofilecounter;

              NewCommentbutton.text="Add Comment";
              NewCommentform.appendChild(NewCommentbutton);
              

               //add Comments Section:

               var CommentsSection = document.createElement("section");
               CommentsSection.classList.add("Comments");
               CommentsSection.id="Commentpost"+postperprofilecounter;
               NewPost.appendChild(CommentsSection);

              //Append new post to local stoarge
              var NewPostjson={};
              NewPostjson.Username = localStorage.getItem('CurrentUserName');
              NewPostjson.postowner = localStorage.getItem('CurrentUserName');//error
              NewPostjson.profileImg= localStorage.getItem('CurrentUserPImge');
              NewPostjson.content= content;
              NewPostjson.likes=0;
              NewPostjson.dislikes=0;
            
              var Postsdata = [];
              Postsdata=JSON.parse(localStorage.getItem('PostsArray'));
              Postsdata.push(NewPostjson);
              localStorage.setItem('PostsArray', JSON.stringify(Postsdata));
              console.log(JSON.parse(localStorage.getItem('PostsArray')));
              console.log("POST ADDED IT WAS NOT NULL");
              }
      
        

           
//comment grow:
function auto_grow(element) {
  element.style.height = "auto";
  element.style.height = (element.scrollHeight)+"px";
}


//Comment Post



function comment(evt)
{ 
        evt.preventDefault();
        var postNo =  parseInt(evt.target.myParam,10);
        window.alert(postNo);
        var textareaID = "NewCommentContent"+postNo;
        window.alert(textareaID);

        var content = document.getElementById(textareaID).value; //Commentcontent
        var PostCommentSectionID = "Commentpost" + postNo;
        var PostCommentSection = document.getElementById(PostCommentSectionID); //get the post to add comment on

        var NewComment = document.createElement("div");
        NewComment.id="comment1" //counter for comments
        NewComment.classList.add("comment");
        PostCommentSection.appendChild(NewComment); //asign comment to post
        var CommentOwner = document.createElement("h3");
        NewComment.appendChild(CommentOwner);
        var CommentownerImg = document.createElement("img");
        CommentownerImg.classList.add("Commentuserpic");
        CommentownerImg.src = localStorage.getItem('CurrentUserPImge');
        CommentOwner.appendChild(CommentownerImg);

        var CommentownerName = document.createElement("span");            
        CommentownerName.classList.add("commentusername");
        CommentownerName.innerText= localStorage.getItem('CurrentUserName');
        CommentOwner.appendChild(CommentownerName);

        var newCommentdiv= document.createElement("div");
        newCommentdiv.classList.add("MyCommentContent");
        NewComment.appendChild(newCommentdiv);


        var newcontent= document.createElement("p");
        newcontent.id="ComentContent1" //to define //error
        newcontent.innerText=content;
        newCommentdiv.appendChild(newcontent);

        // //Append new comment to local stoarge

          var editeddata = [];
          editeddata=JSON.parse(localStorage.getItem('PostsArray'));
          for (var j = 0 ;  j <editeddata.length ; j++)
          {

          }



          editeddata.push(NewPostjson);
        localStorage.setItem('PostsArray', JSON.stringify(Postsdata));
        console.log(JSON.parse(localStorage.getItem('PostsArray')));
        console.log("POST ADDED IT WAS NOT NULL");
        }