Calculator
==========

During the course of an interview process I was asked to build a calculator. At first, I thought, oh that should be simple. Then I began to think about the actual functionality we find in calculators these days, and when you get right down to it there is so much ancillary functionality added to calculators these days that one doesn't really think about until you have to *build it*. Since it's such an open ended requirement, "build a calculator", you have to choose early what kind of features you're going to put into it because you don't want to spend too much time chasing down things that are cool but difficult if it's just a throw-away code project.

So I sat down and thought about the features I wanted to have in this baby calculator. There are the obvious features, numeric buttons, simple mathematical operations +-*/, equals, and there are some of the less obvious (because we just accept them as always there) like memory, clear, delete, history. I decided right away to avoid any of the scientific functions because there simply wouldn't be enough time to get them complete and tested.

After that it was interface time. I constructed the interface using common tools, but I arranged it so that it would at least look presentable. Standard textbox and buttons using nice large-point sans-serif fonts. At first I had decided to add a history (a running computation if you prefer), but during the process of tracking it and attempting to compute it I realized that there were simply too many scenarios to cover for a 4 hour code project so I abandoned the history concept pretty quickly.

Once the interface was finished I began wiring events without functionality. It occurred to me that the numbers would do the exact same thing except for the difference of the digit and the basic operations would do the same thing sequence of things except for the actual mathematical product. So I created my base common events for adding numerics and performing operations. I started off "low rent" by just putting everything about them into these 2 functions. The operations actually took a bit of tweaking because the sequence of events is very important to user interface. One does not simply add to decimals. Oh, yah, the decimal. We always forget that pesky decimal. I had to make sure that operations were changed if 2 operations were selected consecutively, fields were cleared after the selection of an operation, results were displayed with the completion of an operation.

Once the operations were complete, it was time to work on the memory feature. This actually turned out to be an even lower rent version of the calculator itself. I toyed with the idea of making the memory buttons conform to the "standard" operations, but it seemed a bit overkill for such minor features so I created their own events for them and added a label in the corner to show the status of a number in memory. 

Finally, after all features behaved as I wanted I refactored a couple of pieces within the events that looked a little repetitive. They weren't drastic, and I could have left them unfactored. I wanted a slightly cleaner code base though, and for such a simple project I felt it would be amateurish, lazy even, to leave them in their repetitive state. As my professor "Bud" used to say, "Whenever you see repetition like this, your spidey senses should be goin' CRAZY!"

Overall it took about 3 hours or so to complete, and it's still got a few issues I haven't really bothered with to test or to fix. It handles decimals just fine, however I didn't test it with fringe decimals or with infinite repeaters or with 2's complement problems. I set the max length of the number to 8, but I didn't provide any formatting features such as exponential, overflows, or decimal refinement. I also didn't do any "error" checking for numerics that couldn't be adequately display. 

Even though I used a decimal instead of a double, I still used an epsilon comparison for zero equality. Decimal is supposed to be able to handle a zero equality, but I come from the days of double and frankly I'm a little paranoid sometimes.

Overall it has a decent look and feel to me, and it does what I asked it to do. 
