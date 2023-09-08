# Word Processor Server 
 
The [Syncfusion **Word Processor (also known as Document Editor)**](https://www.syncfusion.com/javascript-ui-controls/js-word-processor?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker) is a component with editing capabilities like Microsoft Word. It is used to create, edit, view, and print Word documents. It provides all the common word processing abilities, including editing text; formatting contents; resizing images and tables; finding and replacing text; importing, exporting, and printing Word documents; and using bookmarks and tables of contents. 
 
This Docker project is the predefined Docker container for Syncfusion’s Word Processor backend functionalities. This server-side Web API project is targeting ASP.NET Core 6.0.

If you want to add new functionality or customize any existing functionalities, then create your own Docker file by referencing this Docker project.

The Word Processor is supported in the [JavaScript](https://www.syncfusion.com/javascript-ui-controls?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker), [Angular](https://www.syncfusion.com/angular-ui-components?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker), [React](https://www.syncfusion.com/react-ui-components?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker), [Vue](https://www.syncfusion.com/vue-ui-components?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker), [ASP.NET Core](https://www.syncfusion.com/aspnet-core-ui-controls?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker), [ASP.NET MVC](https://www.syncfusion.com/aspnet-mvc-ui-controls?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker), and [Blazor](https://www.syncfusion.com/blazor-components?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker) platforms.

## Prerequisites	

Have [`Docker`](https://www.docker.com/products/container-runtime#/download) installed in your environment:

* On Windows, install [`Docker for Windows`](https://hub.docker.com/editions/community/docker-ce-desktop-windows).

* On macOS, install [`Docker for Mac`](https://hub.docker.com/editions/community/docker-ce-desktop-windows).

## How to use this Word Processor Docker image

**Step 1:** Pull the word-processor-server image from Docker Hub.

```console
docker pull syncfusion/word-processor-server
```

**Step 2:** Create the docker-compose.yml file with the following code in your file system.

```yaml
version: '3.4' 

services: 
 word-processor-server: 
    image: syncfusion/word-processor-server:latest 
    environment: 
      #Provide your license key for activation
      SYNCFUSION_LICENSE_KEY: YOUR_LICENSE_KEY
    ports:
    - "6002:80"
``` 

**Note:** Word Processor is a commercial product. It requires a valid [license key](https://help.syncfusion.com/common/essential-studio/licensing/licensing-faq/where-can-i-get-a-license-key?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker) to use it in a production environment. Please replace `LICENSE_KEY` with the valid license key in the `docker-compose.yml` file.

**Step 3:** In a terminal tab, navigate to the directory where you've placed the docker-compose.yml file and execute the following.

```console
docker-compose up 
```

Now the Word Processor server Docker instance runs in the localhost with the provided port number `http://localhost:6002`. Open this link in a browser and navigate to the Word Processor Web API `http://localhost:6002/api/documenteditor`. It returns the default get method response. 

**Step 4:** Append the Docker instance running the URL `(http://localhost:6002/api/documenteditor)` to the service URL in the client-side Word Processor control. For more information about how to get started with the Word Processor control, refer to this [`getting started page.`](https://ej2.syncfusion.com/javascript/documentation/document-editor/getting-started?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker)

```html
<!DOCTYPE html>
  <html xmlns="http://www.w3.org/1999/xhtml">
       <head>
          <title>Essential JS 2</title>
          <!-- EJ2 Document Editor dependent material theme -->
          <link href="resources/base/styles/material.css" rel="stylesheet" type="text/css" rel='nofollow' />
          <link href="resources/buttons/styles/material.css" rel="stylesheet" type="text/css" rel='nofollow' />
          <link href="resources/inputs/styles/material.css" rel="stylesheet" type="text/css" rel='nofollow' />
          <link href="resources/popups/styles/material.css" rel="stylesheet" type="text/css" rel='nofollow' />
          <link href="resources/lists/styles/material.css" rel="stylesheet" type="text/css" rel='nofollow' />
          <link href="resources/navigations/styles/material.css" rel="stylesheet" type="text/css" rel='nofollow' />
          <link href="resources/splitbuttons/styles/material.css" rel="stylesheet" type="text/css" rel='nofollow' />
          <link href="resources/dropdowns/styles/material.css" rel="stylesheet" type="text/css" rel='nofollow' />
          <!-- EJ2 DocumentEditor material theme -->
          <link href="resources/documenteditor/styles/material.css" rel="stylesheet" type="text/css" rel='nofollow' />

          <!-- EJ2 Document Editor dependent scripts -->
          <script src="resources/scripts/ej2-base.min.js" type="text/javascript"></script>
          <script src="resources/scripts/ej2-data.min.js" type="text/javascript"></script>
          <script src="resources/scripts/ej2-svg-base.min.js" type="text/javascript"></script>
          <script src="resources/scripts/ej2-file-utils.min.js" type="text/javascript"></script>
          <script src="resources/scripts/ej2-compression.min.js" type="text/javascript"></script>
          <script src="resources/scripts/ej2-pdf-export.min.js" type="text/javascript"></script>
          <script src="resources/scripts/ej2-buttons.min.js" type="text/javascript"></script>
          <script src="resources/scripts/ej2-popups.min.js" type="text/javascript"></script>
          <script src="resources/scripts/ej2-splitbuttons.min.js" type="text/javascript"></script>
          <script src="resources/scripts/ej2-inputs.min.js" type="text/javascript"></script>
          <script src="resources/scripts/ej2-lists.min.js" type="text/javascript"></script>
          <script src="resources/scripts/ej2-navigations.min.js" type="text/javascript"></script>
          <script src="resources/scripts/ej2-dropdowns.min.js" type="text/javascript"></script>
          <script src="resources/scripts/ej2-calendars.min.js" type="text/javascript"></script>
          <script src="resources/scripts/ej2-charts.min.js" type="text/javascript"></script>
          <script src="resources/scripts/ej2-office-chart.min.js" type="text/javascript"></script>
          <!-- EJ2 Document Editor script -->
          <script src="resources/scripts/ej2-documenteditor.min.js" type="text/javascript"></script>
       </head>
<body>
<!--element which is going to render-->
<div id='DocumentEditor' style='height:620px'>

</div>
<script>
    // Initialize DocumentEditorContainer component.
    var documenteditorContainer = new ej.documenteditor.DocumentEditorContainer({ enableToolbar: true });
    ej.documenteditor.DocumentEditorContainer.Inject(ej.documenteditor.Toolbar);
    documenteditorContainer.serviceUrl ="http://localhost:6002/api/documenteditor";
    //DocumentEditorContainer control rendering starts
    documenteditorContainer.appendTo('#DocumentEditor');
</script>
</body>
  </html>
```

## How to configure a spell checker dictionaries path using Docker compose file

**Step 1:** In the `docker-compose.yml` file, you can mount a local directory to a specific path in the container using `volumes` declaration. The command under `volumes` declaration mounts all the files present in the local directory `./data` to the destination directory `/app/data` in the Docker container. 

The local directory `./data` means the directory `data` present in the same parent directory of the `docker-compose.yml` file, like the following screenshot.

![The directory containing the 'docker-compose.yml' file and 'data' folder](https://github.com/SyncfusionExamples/Word-Processor-Server-Docker/blob/master/docker-compose-file-directory.png)

Few resource files are included in the default resource directory `/app/Data` of the Word Processor Docker container. You can find those default resource files from [GitHub](https://github.com/SyncfusionExamples/Word-Processor-Server-Docker/tree/master/src/ej2-documenteditor-server/Data). You can configure a new directory with your own resource files (required spell check dictionaries, and template documents) based on your needs.

If you customize the default resource directory (change in casing, name or directory level), then you must set the modified directory path (assuming `/app/` as home directory) to environment variable `SPELLCHECK_DICTIONARY_PATH` like below.
```yaml
version: '3.4' 
services: 
 word-processor-server: 
    image: syncfusion/word-processor-server:latest 
    environment: 
      #Provide your license key for activation
      SYNCFUSION_LICENSE_KEY: YOUR_LICENSE_KEY
      SPELLCHECK_DICTIONARY_PATH: data
   volumes: 
      -  ./data:/app/data 
    ports:
    - "6002:80"
```

**Step 2:** In the `data` folder, include the dictionary files (.dic, .aff, personal dictionary) and JSON file.

**Note:** For maintaining the custom words for `add to dictionary` option (personal dictionary), place an empty .dic file (e.g.,. customDict.dic file) in the `data` folder.

The JSON file should contain the language-wise spell check dictionary configuration in the following format.
```json
[
  {
    "LanguadeID": 1036, 
    "DictionaryPath": "fr_FR.dic",
    "AffixPath": "fr_FR.aff", 
    "PersonalDictPath": "customDict.dic"
  },
  {
    "LanguadeID": 1033,
    "DictionaryPath": "en_US.dic",
    "AffixPath": "en_US.aff",
    "PersonalDictPath": "customDict.dic"
  }
]
```

By default, the spell checker holds only one language dictionary in memory. If you want to hold multiple dictionaries in memory, then you must set the required count to environment variable `SPELLCHECK_CACHE_COUNT` in Docker compose file. For more information, please refer [spell check documentation](https://ej2.syncfusion.com/documentation/document-editor/spell-check)

A JSON file is included in the default resource directory `/app/Data` of this Docker image with the name `spellcheck.json`. You can add a new file with your own spell check dictionary configurations.

If you customize it, then you must set the new file name to environment variable `SPELLCHECK_JSON_FILENAME` in Docker compose file like below,
```yaml
version: '3.4' 

services: 
 word-processor-server: 
    image: syncfusion/word-processor-server:latest 
    environment: 
      #Provide your license key for activation
      SYNCFUSION_LICENSE_KEY: YOUR_LICENSE_KEY
      SPELLCHECK_DICTIONARY_PATH: data
      SPELLCHECK_CACHE_COUNT: 2
      SPELLCHECK_JSON_FILENAME: spellcheck1.json
    volumes: 
      -  ./data:/app/data  
    ports:
    - "6002:80"
```

Kindly refer these getting started pages to create a Word Processor in [`Angular`](https://ej2.syncfusion.com/angular/documentation/document-editor/getting-started/?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker), [`React`](https://ej2.syncfusion.com/react/documentation/document-editor/getting-started/?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker), [`Vue`](https://ej2.syncfusion.com/vue/documentation/document-editor/getting-started/?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker), [`ASP.NET MVC`](https://ej2.syncfusion.com/aspnetmvc/documentation/document-editor/getting-started/?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker), [`ASP.NET Core`](https://ej2.syncfusion.com/aspnetcore/documentation/document-editor/getting-started/?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker), and [`Blazor`](https://blazor.syncfusion.com/documentation/document-editor/getting-started/server-side-application/?utm_source=docker&utm_medium=listing&utm_campaign=javascript-word-processor-docker).
