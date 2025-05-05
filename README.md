# Advanced_Web_Final_Project
A MVC application for managing events and users participating in them.

## Application Instructions

(Note:Folders are underlines)\
## **<ins>File Structure</ins>**
> <ins>Controllers</ins>
> > EventController
> > EventParticipantController
> > HomeController
> > UserController

> <ins>Migrations</ins>
> > 20250503141620_Mig01.cs
> > ApplicationDbContextModelSnapshot.cs

<ins>Models</ins>
  > <ins>Entities</ins>
  > > Event.cs\
  > > EventParticipant.cs\
  > > User.cs

  > <ins>ViewModels</ins>
  > > EventDetailsVM.cs\
  > > UserDetailsVM.cs  
  > ErrorViewModel

<ins>Services</ins>
> ApplicationDbContext.cs\
> DbEventRepository.cs\
> DbUserRepository.cs\
> IEventParticipantRepository.cs\
> IEventRepository.cs\
> Initializer.cs\
> IUserRepository.cs

<ins>Views</ins>
  > <ins>Event</ins>
  > > Create.cshtml\
  > > Delete.cshtml\
  > > Details.cshtml\
  > > Update.cshtml
  
  > <ins>EventParticipant</ins>
  > > Join.cshtml\
  > > Leave.cshtml\
  > > Participants.cshtml
  
  > <ins>Home</ins>
  > > Index.cshtml\
  > > Privacy.cshtml

  > <ins>Shared</ins>
  > > _Layout.cshtml\
  > > _ValidationScriptsPartial.cshtml\
  > > Error.cshtml

  > <ins>User</ins>
  > > Create.cshtml\
>   > Delete.cshtml\
>   > Index.cshtml\
>   > Update.cshtml

> _ViewImports.cshtml\
> _ViewStart.cshtml   

> <ins>appsetting.json</ins>
> > appsettings.Development.json
