# Student Panel

This is a Blazor WebAssembly project with a separate ASP.NET Core API.

The application shows students, student details, courses, and observed students.


## How to run the API

Open terminal in the project folder and run:

bash cd StudentPanel.Api dotnet run 

The API runs on:

text http://localhost:5001 

Swagger can be opened here:

text http://localhost:5001/swagger 


## How to run the Blazor application

Open another terminal and run:

bash cd StudentPanel.Client dotnet run 

The Blazor app runs on:

text http://localhost:5073 

Main page:

text http://localhost:5073/students 


## Blazor variant

I used Blazor WebAssembly.

I chose it because the app runs in the browser and it is simple for this project.

Also, scoped services work per browser tab, so the observed students state is not shared globally between all users.


## API communication

The typed API client is here:

text StudentPanel.Client/Services/StudentsApiClient.cs 

It is registered in:

text StudentPanel.Client/Program.cs 

The API base URL is stored in:

text StudentPanel.Client/wwwroot/appsettings.json 


## Lifecycle methods

OnInitializedAsync is used in:

text StudentPanel.Client/Pages/Students.razor StudentPanel.Client/Pages/StudentDetails.razor 

In Students.razor, it loads the student list.

In StudentDetails.razor, it loads the course list.


OnParametersSetAsync is used in:

text StudentPanel.Client/Pages/StudentDetails.razor 

It loads student details when the id in the URL changes.


OnAfterRenderAsync is used in:

text StudentPanel.Client/Pages/StudentDetails.razor 

It is used for code that should run after the component is rendered.


## EditForm and validation

Edit forms are used in:

text StudentPanel.Client/Pages/CreateStudent.razor StudentPanel.Client/Pages/StudentDetails.razor 

CreateStudent.razor has validation for creating a student.

StudentDetails.razor has validation for assigning a course.


## State container

The state container is here:

text StudentPanel.Client/Services/ObservedStudentsState.cs 

It stores observed students.

The layout uses this state to show the observed students counter.


## JS Interop

JS Interop is used here:

text StudentPanel.Client/Services/ClipboardService.cs StudentPanel.Client/wwwroot/js/clipboard.js 

It is used for:

- copying email to clipboard
- confirm dialog before removing an observed student


## RenderFragment

The reusable component with RenderFragment<T> is here:

text StudentPanel.Client/Shared/DataTable.razor 

It is used in more than one page:

text StudentPanel.Client/Pages/Students.razor StudentPanel.Client/Pages/StudentDetails.razor StudentPanel.Client/Pages/ObservedStudents.razor 


## ErrorBoundary

ErrorBoundary is used in:

text StudentPanel.Client/Pages/Students.razor StudentPanel.Client/Pages/StudentDetails.razor 

It helps show an error message if a component has an unexpected error.

Normal API or business errors are still shown as normal messages.


## Checklist

The Blazor application starts.

The API starts and returns data.

The student list loads data through HTTP.

The details page works with different id values in the URL.

The create student form validates data.

After creating a student, the app redirects to the details page.

Observed students are stored in shared state.

The observed students counter changes in the layout.

JS Interop works for clipboard and confirm dialog.

The reusable table component is used in several pages.

API errors are shown to the user.

ErrorBoundary is used only for unexpected UI errors.


## README questions

### How is OnInitializedAsync different from OnParametersSetAsync?

OnInitializedAsync runs once when the component is created.

OnParametersSetAsync runs when component parameters are set or changed.

I use OnParametersSetAsync when data depends on the URL id.


### Why do we usually run DOM-dependent code in OnAfterRenderAsync?

Because the HTML page is already rendered at that point.

Before render, the DOM may not exist yet, so JavaScript code that needs the page can fail.


### Why should you be careful with Singleton state in Blazor Server?

A singleton is shared between all users.

If user data is stored in a singleton, different users can see the same data.

That is why user state should usually not be singleton in Blazor Server.


### What does a typed client give you compared to calling HttpClient directly in every component?

A typed client keeps API calls in one place.

Components become cleaner because they call methods like GetStudentsAsync() instead of writing HTTP code everywhere.


### How is NavLink different from a regular <a> link?

A regular <a> link only navigates to another page.

NavLink also adds an active CSS class when the current page matches the link.


### What is RenderFragment<T> used for?

RenderFragment<T> is used to pass a template into a reusable component.

In this project, it is used in DataTable.razor so different pages can define different table rows.


### When does JS Interop make sense, and when is it better to stay with Blazor?

JS Interop makes sense when I need browser features that Blazor does not directly provide.

For example, clipboard and confirm dialog.

It is better to stay with Blazor for normal UI, forms, routing, events, and state.


### What problem does ErrorBoundary solve, and what should it not replace?

ErrorBoundary catches unexpected UI errors and prevents the whole page from breaking.

It should not replace normal error handling, like showing a message when the API returns an error or student is not found.
