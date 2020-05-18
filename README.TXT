A simple library for mapping Json files to models and updating the contents of those Json files.

Example:

JsonRepository repo = new JsonRepository<MyObjectType>(Path);
repo.Update(MyObject);

The object will be serialized and saved to the Path provided.
