![SampleTwo](https://user-images.githubusercontent.com/58665121/215287700-db1d5c57-1d2c-45e6-ba2a-93987faf361a.png)

# Bible Indexer

A comprehensive library for querying bible content and getting cascading dropdowns for loading books of the bible, chapters and associated verses.


## Author

- [King Alex](https://github.com/king-Alex-d-great)


## Badges

[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)
![InstallmentCop](https://user-images.githubusercontent.com/58665121/215287594-c8ae9e26-b377-40f6-869b-91557173807b.png)




## Features
- Get a bible verse
- Get a book of the bible
- Get all verses in a chapter
- Generate a random bible verse
- Get all chapters in a book of the bible
- Get a list of all books and their abbreviations in the bible (For creating dropdowns)
- Get all verses where a specified query string occurs in the bible
- Get a list of numbers representing all chapters in a book of the bible (for creating dropdowns)
- Get a list of numbers representing all verses in a chapter (for creating dropdowns)

## Tech Stack

**C#, .Net6.0, .NetStandard2.1**, 



## How Do I Get Started

First, install NuGet. Then, install BibleIndexer from the package manager console:

```C#   
   NuGet\Install-Package BibleIndexer.Query -Version 1.0.0
```

Or from the .NET CLI as:
```C#   
   dotnet add package BibleIndexer.Query --version 1.0.0
```

Finally, import into the file:
```C#   
   using BibleIndexer;
```
## Doc Reference

### Get chapters in the Book Of a bible

- Sample Usage:

```C#
   await BibleService.GetChaptersInABookOfTheBible(bookName);   
```

- Input

| Parameter  | Type     | Description                         |
| :--------  | :------- | :-------------------------          |
| `bookName` | `string` | **Required**. The name of the book of the bible e.g genesis |

- Output

| Type     |
| :------- |
| `Task<ChaptersResponse?>` |


### Generate a random bible verse
- Sample Usage

```C#   
   await BibleService.GenerateRandomBibleVerse();
```
- Output

| Type     |
| :------- |
| `Task<BibleVerseResponse?>` |

### Get all books of the bible
- Sample Usage

```C#   
   await BibleService.GetAllBooksOfTheBible();
```
- Output

| Type     |
| :------- |
| `Task<object>` |

### Get all verses and verses dropdown in a chapter of the bible
- Sample Usage

```C#   
   await BibleService.GetAllVersesInAChapterOFTheBible(request);
```
- Input

| Parameter  | Type     | 
| :--------  | :------- | 
| `request` | `GetBibleVerseRequest` |  

- Output

| Type     |
| :------- |
| `Task<VersesResponse?>` |

### Get a book of the bible
- Sample Usage

```C#   
   await BibleService.GetBookOfTheBible(bookName);
```
- Input

| Parameter  | Type     | Description                         |
| :--------  | :------- | :-------------------------          |
| `bookName` | `string` | **Required**. The name of the book of the bible e.g genesis |

- Output

| Type     |
| :------- |
| `Task<BlobResponse?>` |

### Get a bible verse
- Sample Usage
```C#   
   await BibleService.GetBibleVerse(request);
```
- Input

| Parameter  | Type     | 
| :--------  | :------- |
| `request` | `GetBibleVerseRequest` | 

- Output

| Type     |
| :------- |
| `Task<BibleVerseResponse?>` |

### Search the bible
- Sample Usage

```C#   
   await BibleService.SearchBible(query);
```
- Input

| Parameter  | Type     | Description                         |
| :--------  | :------- | :-------------------------          |
| `query` | `string` | **Required**. Query param for the search |

- Output

| Type     |
| :------- |
| `Task<IEnumerable<BibleVerseResponse>>` |




## Roadmap


- Robust ReadMe

- AI integration
- Additional features



## Contributing

Contributions are always welcome!

See `contributing.md` for ways to get started.

Please adhere to this project's `code_of_conduct.md`.
