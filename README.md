#IT, hackers, security news - Windows(10) universal app
###Project General Description 
The project is based on Hackers news and Reddit's (netsec) API. Both of the sources use a RESTful design to send data. This implementation is only focusing on the public parts of the API's so no Authentication is required. The application is fairly simple in ui design as well as back-end structure. The key was to create as much reusable code as possible.

### Design
The application is implemented with templates(GridView, ListView, UserControll) in order to fit most of the screen resolutions.
The API clients and abstraction is created in a way which can make the further enhancement of the application functionality fairly easy.  

###Notes 
The application has a basic local storage to save some bandwidth, and keeps the parsed content in "cache" for 15 minutes. This could be further extended to either add a setting for refresh or use this mechanism as save storage for articles. 

###File/folder structure
 **security-hackers-it-news-win10-app/*Page.xaml** - content pages are located in project root. 
 **--||--/Controllers** - controller folder contains all worker classes like AbstractRESTParser.
 **--||--/Models** - contains all the data model classes use in the project like HNewsItemModel
 **Logo** - folder contains the logo and screen designs
 **App_validation** - the certification for current build, test
 
###External sources, useful links

    Hackers news API documentation:
    https://github.com/HackerNews/API

    Reddit Json formatin (API) documentation:
    https://github.com/Reddit/Reddit/wiki/JSON

    Windows Universal app samples:
    https://github.com/Microsoft/Windows-universal-samples

	Windows phone blue book:
	http://www.robmiles.com/journal/2011/10/27/windows-phone-blue-book-available.html
	
 

###Disclaimer
THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.

