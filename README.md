duster.net
==========

Inspired by [duster.js](https://github.com/dmix/dusterjs), this tool lets you auto compile your dust templates inside Visual Studio.

## Example

**hello.html**
```html
<h1>Hello {name}!</h1>
```

**Note**: You can name the template file anything (e.g. hello.dust, hello.template etc.). and the tool will work irrespective of the file's extension. I prefer HTML since that enables Intellisense within Visual Studio. Also enables Emmet, ReSharper specific features if you have those extensions.

**DustJsGenerator** will compile this to **hello.dust.js**:

```js
(function() {
    dust.register("hello", body_0);
 
    function body_0(chk, ctx) {
        return chk.write("<h1>Hello ").reference(ctx._get(false, ["name"]), ctx, "h").write("!</h1>");
    }
    return body_0;
})();
```

**Note**: The name of the template being registerd with dustJs is derived from the file name ("hello" in this example).

You can now include this javascript file in your bundle or as a link on your page:

```
<script src="dust-core-0.3.0.min.js"></script>
<script src="hello.dust.js"></script>
```

To render your template, call:

```html
<script type="text/javascript">
  function render(){
  	dust.render("hello", {name: "Dust JS"}, function (e, html){
  		$('.hello').append(html); 
  	});
  }
</script>
```

Which will output:

```html
<div class="hello"><h1>Hello Dust JS!</h1></div>
```

## Usage

 - Install Visual Studio Extension: [DustJsGenerator](http://visualstudiogallery.msdn.microsoft.com/591bc050-d024-48a1-a79c-ef8009878596).
 - In your solution, select the desired dust template.
 - Under `Properties > Custom Tool`, enter `DustJsGenerator`.  
 
 ![image](https://cloud.githubusercontent.com/assets/781818/3654107/55778cf6-1163-11e4-9ad4-53622cab5696.png)

 - Save the file. This will generate `<filename>.dust.js` file.
 - From now on, the tool will automatically regenerate the compiled template upon saving any changes to the dust template.
