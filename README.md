# WexExercise ReadMe

The following notes describe the intent behind design decisions in this exercise project.

## Runtime

This code has been written in the current version of VisualStudio 18.5.2, and is comiled for .NET 10 runtime.

## Transaction Record

Since there were no requirements given for hosting or larger use-case concerns, my first thought went to an interactive console application. That ultimately seemed out of scope, but I did choose to use integer values for the record identity in order to make manual entry easier to type, as opposed to some form of GUID. This design would need to be revisted if a real use-case was presented.

The thread-locked ID sequencer implemented for this purpose is fairly primitive, and while it is sufficient for this exercise, I would seriously take another look at that in a refactoring pass, or redesign it given more specific requirements.

## Data Storage

The data storage requirements for this exercise were a little uncertain. An in-memeory store seemed ideal for an exercise like this, but the word "persist" does appear in the requirement. I looked for an in-memory database framework that also supports persistent file storage. While I did not end up implementing this option, it could be easily be added.

## Console Application

The project evolved into more of a service library than a proper appliication, which seems acceptable given the lack of a larger use-case for hosting and usage. I have included a primitive console app so the solution can produce a simple executable that demonstrates the essential features of the excercise.

A proper CLI business application could be constructed with the Generic Host framework, including depency injection and other standard application infrastructure. All of that seems out of scope for a small exercise like this.

## Testing Automation

The primary area of uncertainty regarding the requirements for this exercise is the "production testing automation", which was not entirely clear to me. The wording suggested a devops pipeline scenario, which seems more like configuration outside the scope of the code repository. The clarification I received through my recruiter did help a little in this regard.

I have provided test cases, including unit and integration testing, which provide better than 90% code coverage. I hope that is sufficient for this exercise.

## Thank You!

Thanks so much for the opportunity to work on a fun little project like this. I test drove a library I hadn't seen before (StereoDB) and learned a little bit about the US Treasury web API, which was nice.

Thanks for taking the time to look at my submission!

Cheers! --Kurtis
